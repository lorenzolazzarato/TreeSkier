using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    [Header("Jump Info")]

    // Time for jumping
    [Tooltip("How much time the jump lasts")]
    [SerializeField]
    private float _TimeForJump = 4;

    [SerializeField]
    [Tooltip("The time reduction for each difficulty")]
    private float _TimeReductionJump = 1f;

    [Header("Events")]
    [SerializeField]
    private IdContainerGameEvent _JumpMinigameStartEvent;

    [SerializeField]
    private IdContainerGameEvent _JumpMinigameEndEvent;


    private int _difficulty;

    // Jump initiation
    private void JumpInit()
    {
        
        Time.timeScale = ((_TimeForJump - (4 - _difficulty * _TimeReductionJump)) * 100 / _TimeForJump) / 100;
        _JumpMinigameStartEvent.Invoke();
        StartCoroutine(JumpCoroutine());
    }

    // Jump finished
    private void JumpFinish()
    {
        Debug.Log("JumpFinished");
        _JumpMinigameEndEvent.Invoke();
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
}
