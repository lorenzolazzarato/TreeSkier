using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    [Header("Jump Info")]

    // Time for jumping
    [Tooltip("How much time the jump lasts")]
    [SerializeField]
    private float _TimeForJump;

    // How much time gets removed for the difficulty
    [Tooltip("How much time gets removed for the difficulty of the jump")]
    [SerializeField]
    private float _TimeDifferenceForDifficulty = 1;

    // Min time the player has when jumping (ignores difficulty)
    [Tooltip("The minimum time the player has when jumping")]
    [SerializeField]
    private float _MinTimeJump = 0.5f;

    // Max time the player has when jumping (ignores difficulty)
    [Tooltip("The maximum time the player has when jumping")]
    [SerializeField]
    private float _MaxTimeJump = 10f;

    [SerializeField]
    private float _TimeScaleDuringJump = 0.5f;

    [Header("Events")]
    [SerializeField]
    private IdContainerGameEvent _JumpStartEvent;

    [SerializeField]
    private IdContainerGameEvent _JumpEndEvent;


    private int _difficulty;

    // Jump initiation
    private void JumpInit()
    {

        Time.timeScale = _TimeScaleDuringJump;
        _JumpStartEvent.Invoke();
        StartCoroutine(Jumping());
    }

    // Jump finished
    private void JumpFinish()
    {
        Debug.Log("JumpFinished");
        _JumpEndEvent.Invoke();
        Time.timeScale = 1;
    }

    // During jump
    private IEnumerator Jumping()
    {
        float timeToWait = Mathf.Clamp(_TimeForJump - (_difficulty * _TimeDifferenceForDifficulty), _MinTimeJump, _MaxTimeJump);

        // The player is jumping

        yield return new WaitForSecondsRealtime(timeToWait);

        // The player is not jumping anymore

        JumpFinish();
    }

    // Start jump
    public void StartJump(int difficulty = 0)
    {
        _difficulty = difficulty;
        JumpInit();
    }
}
