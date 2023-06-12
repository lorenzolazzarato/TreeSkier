using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _IdProvider;

    [SerializeField]
    private float _MaxTimeToSwipeInSeconds = 1;

    [SerializeField]
    private float _VerticalDistanceForSwipe = 50;

    [SerializeField]
    private float _HorizontalDistanceForSwipe = 50;


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
    }

    private void OnEnable()
    {
        _gameplayInputProvider.OnMove += MoveCharacter;

        _gameplayInputProvider.OnStartTouch += StartTouch;
        _gameplayInputProvider.OnEndTouch += EndTouch;
    }
    private void OnDisable()
    {
        _gameplayInputProvider.OnMove -= MoveCharacter;

        _gameplayInputProvider.OnStartTouch -= StartTouch;
        _gameplayInputProvider.OnEndTouch -= EndTouch;
    }

    private void JumpCharacter()
    {
        Debug.Log("JUMP");
    }

    private void MoveCharacter(Vector2 value)
    {
        //Debug.LogFormat("Moved {0}, {1}", value.x, value.y);

        //Debug.Log("Move Started");

        //Debug.Log(Screen.width / 2);

        if (value.x < Screen.width / 2) 
        {
            Debug.Log("Moving Left");
        }
        else
        {
            Debug.Log("Moving Right");
        }

    }

    private void MoveCharacterCanceled(Vector2 value)
    {
        //Debug.Log("Move canceled");
    }

    private void StartTouch(Vector2 value)
    {
        //Debug.LogFormat("Started touching {0}", value);
        _position = value;
        _timeStart = Time.time;
    }

    private void EndTouch(Vector2 value)
    {

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
}
