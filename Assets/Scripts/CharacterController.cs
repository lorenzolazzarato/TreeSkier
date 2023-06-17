using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum MovementDirection
{
    RIGHT, LEFT, STILL
}

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _IdProvider;

    [Header("Swipe")]

    [SerializeField]
    private float _MaxTimeToSwipeInSeconds = 1;

    [SerializeField]
    private float _VerticalDistanceForSwipe = 50;

    [SerializeField]
    private float _HorizontalDistanceForSwipe = 50;

    [Header("Speed")]

    [SerializeField]
    private float _SlowRatio = 0.0001f;

    [SerializeField]
    private float _AccelerationRatio = 0.0001f;

    [SerializeField]
    private float _MaxSpeed = 1f;

    [Header("Events")]

    [SerializeField]
    private IdContainerGameEvent _HitEvent;

    [SerializeField]
    private IdContainerGameEvent _GatherEvent;

    [Header("Gatherable Containers")]
    [SerializeField]
    private IdContainer _CoinIdContainer;

    [SerializeField]
    private IdContainer _BombIdContainer;



    // Takes
    private MovementDirection _direction = MovementDirection.STILL;

    private float _xSpeed = 0;


    private GameplayInputProvider _gameplayInputProvider;

    // Position saved when the touching starts
    private Vector2 _position;

    // Time when the touching starts
    private float _timeStart;

    // Need the camera to calculate where the touch is
    private ICinemachineCamera _camera;

    private void Awake()
    {
        _gameplayInputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);
    }

    private void Start()
    {
        _camera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;

        //Debug.LogFormat("Value of max speed: {0}", _MaxSpeed);
        //Debug.LogFormat("Value of acceleration: {0}", _AccelerationRatio);
        //Debug.LogFormat("Value of slow: {0}", _SlowRatio);
    }

    private void Update()
    {
        // Calculate how much the player is moving
        if (_direction != MovementDirection.STILL)
        {
            //Debug.Log("Character moving");
            // Add to the speed the acceleration in the correct direction
            _xSpeed += _AccelerationRatio * (_direction == MovementDirection.RIGHT ? 1 : -1);
        }
        else
        {
            //Debug.Log("Character not moving");
            // Else, the speed gradually comes back to zero
            if (_xSpeed > 0)
            {
                _xSpeed -= _SlowRatio;
            }
            else if (_xSpeed < 0)
            {
                _xSpeed += _SlowRatio;
            }
        }



        // Clamp the speed between 2 value
        _xSpeed = Math.Clamp(_xSpeed, -_MaxSpeed, _MaxSpeed);


        // Set the speed to 0 if is less then the AccelerationRatio
        if (Math.Abs(_xSpeed) < _AccelerationRatio)
        {
            _xSpeed = 0;
        }
        //Debug.Log(_xSpeed);

        // Translate the character by the correct amount
        transform.Translate(_xSpeed, 0, 0);


        // Check if the character is out of the screen, in case teleport it to the other side
        if (transform.position.x < -4)
        {
            transform.position = new Vector3(4, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 4)
        {
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
        }



        //Debug.LogFormat("Transform Position {0}", transform.position.x);
    }

    private void OnEnable()
    {
        _gameplayInputProvider.OnMove += MoveCharacter;

        _gameplayInputProvider.OnStartTouch += StartTouch;
        _gameplayInputProvider.OnEndTouch += EndTouch;

        _HitEvent.Subscribe(HitCharacter);
        _GatherEvent.Subscribe(GatherObject);

    }
    private void OnDisable()
    {
        _gameplayInputProvider.OnMove -= MoveCharacter;

        _gameplayInputProvider.OnStartTouch -= StartTouch;
        _gameplayInputProvider.OnEndTouch -= EndTouch;

        _HitEvent.Unsubscribe(HitCharacter);
        _GatherEvent.Unsubscribe(GatherObject);

    }

    private void JumpCharacter()
    {
        Debug.Log("JUMP");
    }

    private void MoveCharacter(Vector2 value)
    {
        // Select the correct direction of the movement
        if (value.x < Screen.width / 2)
        {
            _direction = MovementDirection.LEFT;
        }
        else
        {
            _direction = MovementDirection.RIGHT;
        }

    }

    private void MoveCharacterCanceled(Vector2 value)
    {
        //Debug.Log("Move canceled");
    }

    private void StartTouch(Vector2 value)
    {
        //Debug.LogFormat("Started touching {0}", value);
        MoveCharacter(value);
        _position = value;
        _timeStart = Time.time;
    }

    private void EndTouch(Vector2 value)
    {
        // Stop the player movement
        _direction = MovementDirection.STILL;

        //Debug.LogFormat("Ended touching {0}", value);
        float endTime = Time.time;

        CheckSwipe(_position, value, endTime - _timeStart);
    }

    // Check the direction of a swipe
    private void CheckSwipe(Vector2 start, Vector2 finish, float time)
    {
        // Count the swipe only if the y change is high enough (ignore little swipe)
        // And if the swipe is fast enough

        //Debug.LogFormat("Checking the swipe, time for the swipe {0}", time);

        if (Math.Abs(start.y - finish.y) > _VerticalDistanceForSwipe && Math.Abs(start.x - finish.x) < _HorizontalDistanceForSwipe && time < _MaxTimeToSwipeInSeconds)
        {
            //Debug.Log("Swipe Counts");

            // Calculating the direction of the swipe
            if (start.y - finish.y < 0)
            {
                //Debug.Log("Swipe up");
                JumpCharacter();
            }
            else
            {

                //Debug.Log("Swipe down");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<MoovableObject>().HitObject();
    }

    private void HitCharacter(GameEvent evt)
    {

        Debug.Log("Character hit");
    }

    // Function called when an object is gathered
    private void GatherObject(GameEvent evt)
    {
        //Debug.Log("Gathered object");
        GatherableEvent gEvt = (GatherableEvent)evt;

        if (gEvt != null)
        {
            //Debug.Log(gEvt.gatheredObject._ObjectIdContainer);
            if (gEvt.gatheredObject == _CoinIdContainer)
                CoinGathered();
            else if (gEvt.gatheredObject == _BombIdContainer)
                BombGathered();
        }
        else
        {
            Debug.Log("Event is not a gatherable event");
            return;
        }


    }

    private void CoinGathered()
    {
        Debug.Log("Coin Gathered");
    }

    private void BombGathered()
    {
        Debug.Log("Bomb Gathered");
    }
}
