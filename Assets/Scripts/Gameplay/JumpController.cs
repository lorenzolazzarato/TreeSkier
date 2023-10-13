using System.Collections;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    [Header("Jump Info")]


    [SerializeField]
    private BaseJumpScriptable _BaseJumpScriptable;

    [Header("Events")]
    [SerializeField]
    private IdContainerGameEvent _JumpMinigameStartEvent;

    [SerializeField]
    private JumpEndEvent _JumpMinigameEndEvent;

    [SerializeField]
    private EndMinigameEvent _FinishMinigameEvent;

    [SerializeField]
    private IdContainerGameEvent _HitEvent;

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

    private float _timeReductionJumpEasy;
    private float _timeReductionJumpMedium;
    private float _timeReductionJumpHard;

    private HardJump _myHardJumpPrefab;

    private EasyJump _myEasyJumpPrefab;

    private void OnEnable()
    {
        _easyMode = FlowSystem.Instance.GetFSMVariable<bool>("EasyMode");

        _timeForJump = _BaseJumpScriptable.InitialTimeForJump;
        _timeReductionJumpEasy = _BaseJumpScriptable.TimeReductionForEasy;
        _timeReductionJumpMedium = _BaseJumpScriptable.TimeReductionForMedium;
        _timeReductionJumpHard = _BaseJumpScriptable.TimeReductionForHard;

        _FinishMinigameEvent.Subscribe(MinigameEnd);

    }

    private void OnDisable()
    {
        _FinishMinigameEvent.Unsubscribe(MinigameEnd);
    }

    // Start jumping and call either the minigame jump or the simple jump functions
    public void StartJump(int difficulty = 0, bool minigame = false)
    {
        //Debug.Log("Jump started " + difficulty);

        if (minigame)
        {
            _difficulty = difficulty;
            MinigameStart();
        }
        else
        {
            _difficulty = 4;
            StartCoroutine(SimpleJumpCoroutine());
        }
    }

    // Jump initiation when jumping on a ramp
    private void MinigameStart()
    {
        //Change input provider
        InputSystem.Instance.DisableInputProvider(_GameplayProviderContainer.Id);
        InputSystem.Instance.EnableInputProvider(_MinigameProviderContainer.Id);

        _hasTouched = false;

        float jumpTime = _timeForJump;

        switch (_difficulty)
        {
            case 1:
                jumpTime -= _timeReductionJumpEasy;
                break;

            case 2:
                jumpTime -= _timeReductionJumpMedium;
                break;
            case 3:
                jumpTime -= _timeReductionJumpHard;
                break;

            default:
                break;
        }


        _JumpMinigameStartEvent.Invoke();
        _isMinigameStarted = true;
        //StartCoroutine(JumpCoroutine());
        if (_easyMode)
        {
            EasyJump(jumpTime);
        }
        else
        {
            HardJump(jumpTime);
        }

    }

    // End the jump and restore gameplay provider
    private void EndJump()
    {
        // Debug.Log("JumpFinished");
        _JumpMinigameEndEvent.Invoke();
        _isMinigameStarted = false;
        Time.timeScale = 1;
        // Debug.Log(InputSystem.Instance.name);
        InputSystem.Instance.DisableInputProvider(_MinigameProviderContainer.Id);
        InputSystem.Instance.EnableInputProvider(_GameplayProviderContainer.Id);
    }

    // During jump from a Ramp
    private IEnumerator SimpleJumpCoroutine()
    {
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
        _JumpMinigameEndEvent.IsMinigame = false;
        _JumpMinigameEndEvent.MinigamePassed = false;

        EndJump();
    }

    public void CheckPositionTouched(Vector2 position)
    {
        // Based on the type of minigame, do different things
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


    }

    public void TouchEnded()
    {
        //Touch ended when in hard mode

        //Debug.Log("TouchEnded");
        if (!_easyMode && _myHardJumpPrefab != null)
        {
            _myHardJumpPrefab.ResetSnowflakes();
        }
    }

    private void MinigameEnd(GameEvent evt)
    {
        EndMinigameEvent ev = (EndMinigameEvent)evt;
        if (ev != null)
        {
            _minigamePassed = ev.minigamePassed;
        }

        //If minigame passed add the points
        if (_minigamePassed)
        {
            int score;
            switch (_difficulty)
            {
                case 1:
                    score = 300;
                    break;

                case 2:
                    score = 600;
                    break;

                case 3:
                    score = 1000;
                    break;

                default:
                    score = 300;
                    break;
            }

            ScoreManager.Instance.AddScore(score);

            _JumpMinigameEndEvent.MinigamePassed = true;
        }
        else
        {
            _JumpMinigameEndEvent.MinigamePassed = false;
            //If the minigames is not passed, hit the player
            _HitEvent.Invoke();
        }

        _JumpMinigameEndEvent.IsMinigame = true;
        EndJump();
    }

    private void EasyJump(float jumpTime)
    {
        _myEasyJumpPrefab = Instantiate(_EasyJumpPrefab);
        _myEasyJumpPrefab.InitEasyJump(jumpTime, _difficulty);
    }

    private void HardJump(float jumpTime)
    {
        _myHardJumpPrefab = Instantiate(_HardJumpPrefab);
        _myHardJumpPrefab.Init(jumpTime, _difficulty);

    }
}
