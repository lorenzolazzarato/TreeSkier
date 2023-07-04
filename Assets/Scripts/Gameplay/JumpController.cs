using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    [Header("Jump Info")]

    // Time for jumping
    [Tooltip("How much time the jump lasts")]
    [SerializeField]
    private float _TimeForJump = 2;

    [SerializeField]
    [Tooltip("The time reduction for each difficulty")]
    private float _TimeReductionJump = .4f;

    [Header("Events")]
    [SerializeField]
    private IdContainerGameEvent _JumpMinigameStartEvent;

    [SerializeField]
    private IdContainerGameEvent _JumpMinigameEndEvent;

    [Header("Easy Jump")]
    [SerializeField]
    private float _EasyJumpMinAcceptanceTime;

    [SerializeField]
    private float _EasyJumpMaxAcceptanceTime;

    private int _difficulty;

    private bool _hasTouched;

    private bool _isMinigameStarted = false;

    private bool _easyMode = true;

    private bool _minigamePassed = false;

    private void OnEnable()
    {
        _EasyJumpMaxAcceptanceTime = Mathf.Clamp(_EasyJumpMaxAcceptanceTime, _EasyJumpMinAcceptanceTime, _TimeForJump);
    }

    // Jump initiation for the minigame
    private void JumpInit()
    {
        _hasTouched = false;
        Time.timeScale = ((_TimeForJump - (_TimeForJump - _difficulty * _TimeReductionJump)) * 100 / _TimeForJump) / 100;
        _JumpMinigameStartEvent.Invoke();
        _isMinigameStarted = true;
        StartCoroutine(JumpCoroutine());
        if (_easyMode)
        {
            EasyJump();
        }
        
    }

    // Jump finished
    private void JumpFinish()
    {
        Debug.Log("JumpFinished");
        _JumpMinigameEndEvent.Invoke();
        _isMinigameStarted = false;
        Time.timeScale = 1;
    }

    // During jump from a Ramp (time slows and minigame start)
    private IEnumerator JumpCoroutine()
    {
        // Reduce the jump time based on the difficulty of the jump
        // float timeToReduce = (_difficulty * _TimeDifferenceForDifficulty);
        // float timeToWait = Mathf.Clamp(_TimeForJump - timeToReduce, _MinTimeJump, _MaxTimeJump);

        // The player is jumping

        yield return new WaitForSeconds(_TimeForJump);

        // The player is not jumping anymore

        JumpFinish();
    }

    // Start jump
    public void StartJump(int difficulty = 0, bool minigame = false)
    {
        // If we need to start the minigame, call jump init
        if (minigame)
        {
            _difficulty = difficulty;
            JumpInit();
        }
        else
        {
            // If there is no minigame, don't slow time
            _difficulty = 4;
            StartCoroutine(JumpCoroutine());
        }
    }

    public void CheckPositionTouched(Vector2 position)
    {

        // Easy mode
        if (!_hasTouched && _isMinigameStarted && _easyMode)
        {
            _hasTouched = true;
            Debug.Log("Touched screen while minigame active");
            
        }

        // Based on the type of minigame, do different things
    }

    private void EasyJump()
    {
        StartCoroutine(EasyJumpCoroutine());
    }

    // Coroutine that checks if the player has touched at the right time
    IEnumerator EasyJumpCoroutine()
    {
        for (float timePassed = 0; timePassed < _TimeForJump; timePassed += .1f)
        {

            if (_hasTouched)
            {
                _minigamePassed = timePassed > _EasyJumpMinAcceptanceTime && timePassed < _EasyJumpMaxAcceptanceTime;
                Debug.Log("minigame ended for touch");
                Debug.Log(_minigamePassed);
                break;
            }

            yield return new WaitForSeconds(.1f);

        }
    }
}
