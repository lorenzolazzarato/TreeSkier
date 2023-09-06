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
    [Header("Input providers")]
    [SerializeField]
    private IdContainer _GameplayIdProvider;

    [SerializeField]
    private IdContainer _MinigameIdProvider;

    [Header("Player Lives")]

    [SerializeField]
    private IntSO _PlayerMaxLife;

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

    [Tooltip("Hit event for the player")]
    [SerializeField]
    private IdContainerGameEvent _HitEvent;

    [Tooltip("Life up event for the player")]
    [SerializeField]
    private IdContainerGameEvent _LifeUpEvent;

    [Tooltip("Gather event for the player")]
    [SerializeField]
    private IdContainerGameEvent _GatherEvent;

    [Tooltip("Change lives event")]
    [SerializeField]
    private ChangeLivesEvent _ChangeLivesEvent;

    [Tooltip("Event triggered when the jump starts from the jump controller")]
    [SerializeField]
    private IdContainerGameEvent _JumpStartEvent;

    [Tooltip("Event triggered when the jump ends from the jump controller")]
    [SerializeField]
    private IdContainerGameEvent _JumpEndEvent;


    [SerializeField]
    private RampHitEvent _RampHitEvent;

    [Header("Gatherable Containers")]
    [SerializeField]
    private IdContainer _CoinIdContainer;

    [SerializeField]
    private IdContainer _BombIdContainer;

    [SerializeField]
    private IdContainer _FlagIdContainer;

    // SPRITES
    [Header("Sprites")]
    [SerializeField]
    private Sprite _RunningSprite;

    [SerializeField]
    private Sprite _OuchSprite;

    [Header("Jump Controller")]

    [SerializeField]
    private JumpController _JumpController;

    [SerializeField]
    private SpriteRenderer _SpriteRenderer;

    [Header("Jump attributes")]

    [Tooltip("Maximum time the player has to jump when encountering a ramp")]
    [SerializeField]
    private float _JumpAcceptanceDuration = 1;

    [SerializeField]
    private BaseJumpScriptable _BaseJumpScriptable;

    [SerializeField]
    private float _JumpHeight = 2f;

    [Header("Animations")]
    [SerializeField]
    private Animator _PlayerAnimator;
    [SerializeField]
    private float _OuchTime;


    // Takes
    private MovementDirection _direction = MovementDirection.STILL;

    private float _xSpeed = 0;


    private GameplayInputProvider _gameplayInputProvider;
    private MinigameInputProvider _minigameInputProvider;

    // Position saved when the touching starts
    private Vector2 _position;

    // Time when the touching starts
    private float _timeStart;


    // Variable used to handle the minigame start
    private bool _canMinigameStart = false;

    private bool _canJump = true;

    private bool _isJumping = false;

    private int _jumpDifficulty = 0;

    private int _playerLife;



    private void Awake()
    {
        _gameplayInputProvider = InputSystem.Instance.GetInput<GameplayInputProvider>(_GameplayIdProvider.Id);
        _minigameInputProvider = InputSystem.Instance.GetInput<MinigameInputProvider>(_MinigameIdProvider.Id);

        InputSystem.Instance.EnableInputProvider(_GameplayIdProvider.Id);
        InputSystem.Instance.DisableInputProvider(_MinigameIdProvider.Id);


        Debug.Log(Camera.main.WorldToScreenPoint(new Vector3(4.24f, 0, -2)));
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 8)));
        
        //_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //Debug.Log("Sprite renderer loaded");

        _playerLife = _PlayerMaxLife.value;
    }

    private void Start()
    {


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
        transform.Translate(_xSpeed * Time.deltaTime, 0, 0);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        // Check if the character is out of the screen, in case teleport it to the other side
        if (screenPos.x < 0 || screenPos.x > Screen.width)
        {
            //Debug.Log(transform.position.x);
            Debug.Log(Camera.main.ScreenToWorldPoint(screenPos));
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        



        //Debug.LogFormat("Transform Position {0}", transform.position.x);
    }

    private void OnEnable()
    {
        //Gameplay
        _gameplayInputProvider.OnMove += MoveCharacter;

        _gameplayInputProvider.OnStartTouch += StartTouch;
        _gameplayInputProvider.OnEndTouch += EndTouch;

        //Minigame
        _minigameInputProvider.OnMove += MoveCharacter;

        _minigameInputProvider.OnStartTouch += StartTouch;
        _minigameInputProvider.OnEndTouch += EndTouch;

        _HitEvent.Subscribe(HitCharacter);
        _JumpEndEvent.Subscribe(OnJumpEnd);
        _JumpStartEvent.Subscribe(OnMinigameStart);
        _RampHitEvent.Subscribe(OnRampHitEvent);
        _LifeUpEvent.Subscribe(LifeUp);


    }
    private void OnDisable()
    {
        _gameplayInputProvider.OnMove -= MoveCharacter;

        _gameplayInputProvider.OnStartTouch -= StartTouch;
        _gameplayInputProvider.OnEndTouch -= EndTouch;

        _minigameInputProvider.OnMove -= MoveCharacter;

        _minigameInputProvider.OnStartTouch -= StartTouch;
        _minigameInputProvider.OnEndTouch -= EndTouch;


        _HitEvent.Unsubscribe(HitCharacter);
        _JumpEndEvent.Unsubscribe(OnJumpEnd);
        _JumpStartEvent.Unsubscribe(OnMinigameStart);
        _RampHitEvent.Unsubscribe(OnRampHitEvent);
        _LifeUpEvent.Unsubscribe(LifeUp);

    }

    private void JumpCharacter()
    {
        //Debug.Log("JUMP");

        if (_canJump)
        {

            gameObject.layer = LayerMask.NameToLayer("Air");

            _JumpController.StartJump(_jumpDifficulty, _canMinigameStart);
            
            _canJump = false;

            Debug.Log("Jump started");
            _isJumping = true;

            StartCoroutine(JumpAnimationStart());
        }

    }

    private void MoveCharacter(Vector2 value)
    {
        //Debug.Log("move character");
        if (_isJumping)
        {
            
            _JumpController.CheckPositionTouched(value);
                                                                    
        }
        else
        {

            // Select the correct direction of the movement
            if (value.x < Screen.width / 2) {
                _direction = MovementDirection.LEFT;
            } else {
                _direction = MovementDirection.RIGHT;
            }


            // flip sprite if not facing current direction
            if (_direction == MovementDirection.RIGHT && !_SpriteRenderer.flipX) {
                _SpriteRenderer.flipX = true;
            } else if (_direction == MovementDirection.LEFT && _SpriteRenderer.flipX) {
                _SpriteRenderer.flipX = false;
            }
        }

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
        //Debug.Log("End touch");

        // Stop the player movement
        _direction = MovementDirection.STILL;

        //Debug.LogFormat("Ended touching {0}", value);
        float endTime = Time.time;

        if (_isJumping)
        {
            _JumpController.TouchEnded();
        }

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
        collision.GetComponentInParent<MovableObject>().HitObject();
    }

    private void HitCharacter(GameEvent evt)
    {
        //Debug.Log("Character hit");
        StartCoroutine(OuchSpriteAnimation());

        // remove half heart and check game over
        _playerLife -= 1;
        _ChangeLivesEvent.numberOfLives = _playerLife;
        _ChangeLivesEvent.Invoke();

        if (_playerLife <= 0) {
            Debug.Log("GAME OVER");
            FlowSystem.Instance.TriggerFSMEvent("GAMEOVER");
        }
    }

    private void LifeUp(GameEvent evt) {
        // add half heart
        if( _playerLife < _PlayerMaxLife.value) {
            _playerLife += 1;
            _ChangeLivesEvent.numberOfLives = _playerLife;
            _ChangeLivesEvent.Invoke();
        }
        
    }

    private void OnJumpEnd(GameEvent evt)
    {
        Debug.Log("Jump end event called");
        _isJumping = false;
        _direction = MovementDirection.STILL;
        StartCoroutine(JumpAnimationEnd());
    }

    private void OnMinigameStart(GameEvent evt)
    {
        gameObject.layer = LayerMask.NameToLayer("Minigame");
    }

    private void OnRampHitEvent(GameEvent evt)
    {
        //Debug.Log("Ramp hit event from player");
        RampHitEvent rampEvent = (RampHitEvent)evt;
        if (rampEvent != null)
        {
            _jumpDifficulty = rampEvent.difficulty;
        }
        StartCoroutine(HandleTimeToJump());
        
    }

    // Function to handle the jump acceptance
    IEnumerator HandleTimeToJump()
    {
        _canMinigameStart = true;
        yield return new WaitForSeconds(_JumpAcceptanceDuration);
        _canMinigameStart = false;
        _jumpDifficulty = 0;

    }

    IEnumerator OuchSpriteAnimation() {
        _PlayerAnimator.SetTrigger("OnOuchEnter");
        yield return new WaitForSeconds(_OuchTime);
        _PlayerAnimator.SetTrigger("OnOuchExit");
    }

    IEnumerator JumpAnimationStart()
    {
        Debug.Log("current z: " + transform.position.z);
        
        float maxTime = _BaseJumpScriptable.JumpDurationWithoutMinigame / 2;
        for (float t = 0; t < maxTime; t += Time.deltaTime)
        {
            transform.Translate(0, 0, - Time.deltaTime * _JumpHeight);
            yield return null;
        }
        Debug.Log("final z: " + transform.position.z);

    }

    IEnumerator JumpAnimationEnd()
    {
        if (_RampHitEvent.difficulty == 3) { // jump performed on a hard ramp: cool jump
            _PlayerAnimator.SetTrigger("OnCoolJumpEnter");
        }
        else _PlayerAnimator.SetTrigger("OnJumpEnter");

        float maxTime = _BaseJumpScriptable.JumpDurationWithoutMinigame / 2;
        for (float t = 0; t < maxTime; t += Time.deltaTime)
        {
            transform.Translate(0, 0, Time.deltaTime * _JumpHeight);
            yield return null;
        }
        gameObject.layer = LayerMask.NameToLayer("Ground-Air");
        _canJump = true;
        Debug.Log("final z: " + transform.position.z);
    }
}
