using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    [Header("Jump Info")]

    // Time for jumping

    [SerializeField]
    private BaseJumpScriptable _BaseJumpScriptable;

    [Header("Events")]
    [SerializeField]
    private IdContainerGameEvent _JumpMinigameStartEvent;

    [SerializeField]
    private IdContainerGameEvent _JumpMinigameEndEvent;

    [SerializeField]
    private EndMinigameEvent _FinishMinigameEvent;

    [Header("Easy Jump")]
    [SerializeField]
    private EasyJumpScriptable _EasyJumpScriptable;

    [SerializeField]
    private EasyJump _EasyJumpPrefab;

    [Header("Hard Jump")]
    [SerializeField]
    private HardJumpScriptable _HardJumpScriptable;

    [SerializeField]
    private HardJump _HardJumpPrefab;

    [Header("Input Providers")]
    [SerializeField]
    private IdContainer _MinigameProviderContainer;

    [SerializeField]
    private IdContainer _GameplayProviderContainer;


    private int _difficulty;

    private bool _hasTouched;

    private bool _isMinigameStarted = false;

    private bool _easyMode = true;

    private bool _minigamePassed = false;

    private float _easyJumpMinAcceptanceTime;

    private float _easyJumpMaxAcceptanceTime;

    private float _timeForJump;

    private float _timeReductionJump;

    private HardJump _myHardJumpPrefab;

    private EasyJump _myEasyJumpPrefab;

    private void OnEnable()
    {
        _easyMode = _BaseJumpScriptable.EasyMode;

        _timeForJump = _BaseJumpScriptable.InitialTimeForJump;
        _timeReductionJump = _BaseJumpScriptable.TimeReductionForDifficulty;

        _easyJumpMinAcceptanceTime = _EasyJumpScriptable._EasyJumpMinAcceptanceTime;
        _easyJumpMaxAcceptanceTime = _EasyJumpScriptable._EasyJumpMaxAcceptanceTime;

        _FinishMinigameEvent.Subscribe(EndMinigame);

        // Max acceptance time must be >= than Min acceptance time
        _easyJumpMaxAcceptanceTime = Mathf.Clamp(_easyJumpMaxAcceptanceTime, _easyJumpMinAcceptanceTime, _timeForJump);
    }

    // Jump initiation for the minigame
    private void JumpInit()
    {

        InputSystem.Instance.DisableInputProvider(_GameplayProviderContainer.Id);
        InputSystem.Instance.EnableInputProvider(_MinigameProviderContainer.Id);

        _hasTouched = false;
        
        //Debug.Log("Time for jump " + _timeForJump);
        _BaseJumpScriptable.TimeForJump = _timeForJump - _timeReductionJump * _difficulty;
        //Time.timeScale = ((_timeForJump - (_timeForJump - _difficulty * _timeReductionJump)) * 100 / _timeForJump) / 100;

        Debug.Log("Time for jump " + _BaseJumpScriptable.TimeForJump);
        _JumpMinigameStartEvent.Invoke();
        _isMinigameStarted = true;
        //StartCoroutine(JumpCoroutine());
        if (_easyMode)
        {
            EasyJump();
        }
        else
        {
            HardJump();
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
        //Debug.Log(InputSystem.Instance.name);
        InputSystem.Instance.DisableInputProvider(_MinigameProviderContainer.Id);
        InputSystem.Instance.EnableInputProvider(_GameplayProviderContainer.Id);
    }

    // During jump from a Ramp
    private IEnumerator JumpCoroutine()
    {
        // Reduce the jump time based on the difficulty of the jump
        // float timeToReduce = (_difficulty * _TimeDifferenceForDifficulty);
        // float timeToWait = Mathf.Clamp(_TimeForJump - timeToReduce, _MinTimeJump, _MaxTimeJump);

        // The player is jumping

        if (_difficulty == 4)
        {
            yield return new WaitForSeconds(_BaseJumpScriptable.JumpDurationWithoutMinigame / 2);
        }
        else
        {
            yield return new WaitForSeconds(_timeForJump);
        }


        // The player is not jumping anymore

        JumpFinish();
    }

    // Start jump and call the right function based on the jump type
    public void StartJump(int difficulty = 0, bool minigame = false)
    {
        Debug.Log("Jump started " + difficulty);

        // If we need to start the minigame, call jump init
        if (minigame)
        {
            _difficulty = difficulty;
            JumpInit();
        }
        else
        {
            _difficulty = 4;
            StartCoroutine(JumpCoroutine());
        }
    }

    public void CheckPositionTouched(Vector2 position)
    {
        Debug.Log("Checking position touched");

        // Easy mode
        if (!_hasTouched && _isMinigameStarted && _easyMode)
        {
            _hasTouched = true;
            //Debug.Log("Touched screen while minigame active");
            _myEasyJumpPrefab.Touched();
            
        }
        // Hard mode
        else if (_isMinigameStarted)
        {
            //Debug.Log("Checking hard minigame position " + position);
            _myHardJumpPrefab.CheckPosition(position);
        }

        

        // Based on the type of minigame, do different things
    }

    public void TouchEnded()
    {

        Debug.Log("TouchEnded");
        if (!_easyMode && _myHardJumpPrefab != null)
        {
            _myHardJumpPrefab.ResetSnowflakes();
        }
    }


    private void EasyJump()
    {
        _myEasyJumpPrefab = Instantiate(_EasyJumpPrefab);
        
    }

    

    private void HardJump()
    {
        _myHardJumpPrefab = Instantiate(_HardJumpPrefab);
        _myHardJumpPrefab.Init(_difficulty);
        
    }

    
    private void EndMinigame(GameEvent evt)
    {
        EndMinigameEvent ev = (EndMinigameEvent)evt;

        if (ev != null)
        {
            _minigamePassed = ev.minigamePassed;
        }

        Debug.Log("Minigame passed: " + _minigamePassed);

        JumpFinish();
    }
}
