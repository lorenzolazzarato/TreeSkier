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
    private EasyJumpScriptable _EasyJumpScriptable;

    private int _difficulty;

    private bool _hasTouched;

    private bool _isMinigameStarted = false;

    private bool _easyMode = true;

    private bool _minigamePassed = false;

    private float _easyJumpMinAcceptanceTime;

    private float _easyJumpMaxAcceptanceTime;

    private void OnEnable()
    {
        _easyJumpMinAcceptanceTime = _EasyJumpScriptable._EasyJumpMinAcceptanceTime;
        _easyJumpMaxAcceptanceTime = _EasyJumpScriptable._EasyJumpMaxAcceptanceTime;

        // Max acceptance time must be >= than Min acceptance time
        _easyJumpMaxAcceptanceTime = Mathf.Clamp(_easyJumpMaxAcceptanceTime, _easyJumpMinAcceptanceTime, _TimeForJump);
    }

    // Jump initiation for the minigame
    private void JumpInit()
    {
        _hasTouched = false;
        // Set the time scale to the correct value
        /*
         * The formula is the following:
         * Time for jump indicates how long does the jump last normally with
         * The time scale then gets reduced based on the difficulty of the jump
         * And finally it gets put in percentage
         * Example -> time to jump is 4 seconds, difficulty is easy (1)
         * So the jump get slowed in a way that the player has more time to complete the minigame
         * If the difficulty is harder, the time scale is closer to 1 and the player has less time to complete the minigame
         */
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
        // The jump is finished, the time scale gets reset to 1 and invoke the event
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

    // Start jump and call the right function based on the jump type
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
    // (Considering easy jump as the circle that closes)
    IEnumerator EasyJumpCoroutine()
    {
        for (float timePassed = 0; timePassed < _TimeForJump; timePassed += .1f)
        {

            if (_hasTouched)
            {
                _minigamePassed = timePassed > _easyJumpMinAcceptanceTime && timePassed < _easyJumpMaxAcceptanceTime;
                Debug.Log("minigame ended for touch");
                Debug.Log(_minigamePassed);
                break;
            }

            yield return new WaitForSeconds(.1f);

        }
    }
}
