using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardJump : MonoBehaviour
{

    [SerializeField]
    private HardJumpSnowflake _Snowflake;

    [SerializeField]
    private BaseJumpScriptable _BaseJumpInfo;

    [SerializeField]
    private JumpBase _JumpBaseScript;

    [SerializeField]
    private HardJumpScriptable _HardJumpInfo;

    [SerializeField]
    private EndMinigameEvent _EndMinigameEvent;


    private int _difficulty;

    private int _baseSnowflakesNumber;

    private List<HardJumpSnowflake> _snowflakes = new List<HardJumpSnowflake>();

    private HardJumpSnowflake _tempSnowflake;

    private float _jumpTime;

    private Camera _camera;

    private int _snoflakesTouched = 0;

    private bool _minigamePassed = false;

    private void Awake()
    {
        //Init(1);
    }

    [ContextMenu("InitStart")]
    public void Init(float jumpTime, int difficulty = 1)
    {
        //Debug.Log("initialized hard jump");
        _difficulty = difficulty;
        _baseSnowflakesNumber = _HardJumpInfo.BaseSnowflakesNumber;
        _jumpTime = jumpTime;

        _JumpBaseScript.InitBaseJump(_jumpTime);

        _camera = Camera.main;
        

        GenerateSnowflakes();
        StartCoroutine(MinigameStart());
    }

    private void GenerateSnowflakes()
    {
        //Debug.Log("Generating snowflakes");

        for (int i = 0; i < _difficulty + _baseSnowflakesNumber; ++i)
        {
            _tempSnowflake = Instantiate(_Snowflake);

            _tempSnowflake.transform.position = new Vector3(UnityEngine.Random.Range(-3.7f, 3.7f), UnityEngine.Random.Range(-5f, 5f), -2);
            

            
            //Debug.Log(_camera.WorldToScreenPoint(_tempSnowflake.transform.position));

            //Debug.Log("Generated snowflake");

            _snowflakes.Add(_tempSnowflake);
            //Debug.Log("Added snowflake " + _snowflakes.Count);
        }
    }

    // Check if the position given is inside of a snowflake
    public bool CheckPosition(Vector2 pos)
    {
        //Debug.Log("Position to check");
        //Debug.Log(pos);

        float width, height;

        width = _Snowflake.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        height = _Snowflake.GetComponentInChildren<SpriteRenderer>().bounds.size.y;

        //Debug.Log("Width and height: " + width + " - " + height);

        //Debug.Log("snowflakes number " + _snowflakes.Count);

        foreach (HardJumpSnowflake sn in _snowflakes)
        {
            if (sn != null)
            {

            Vector3 startPos = new Vector3(sn.transform.position.x - width / 2, sn.transform.position.y - height / 2);
            Vector3 endPos = new Vector3(sn.transform.position.x + width / 2, sn.transform.position.y + height / 2);

            float startX = _camera.WorldToScreenPoint(startPos).x;
            float endX = _camera.WorldToScreenPoint(endPos).x;

            float startY = _camera.WorldToScreenPoint(startPos).y;
            float endY = _camera.WorldToScreenPoint(endPos).y;

            //Debug.Log("Positions: " + startX + " - " + endX + " - " + startY + " - " + endY + " - " + pos);

                if ((pos.x >= startX && pos.x <= endX) && (pos.y >= startY && pos.y <= endY) && sn.gameObject.activeSelf)
                {
                    //Debug.Log("Presoooooo");
                    _snoflakesTouched++;
                    if (_snoflakesTouched == _snowflakes.Count)
                    {
                        _minigamePassed = true;
                    }
                    sn.gameObject.SetActive(false);
                    return true;
                }
            
            }

        }

        return false;
    }

    public void ResetSnowflakes()
    {
        foreach (HardJumpSnowflake sn in _snowflakes)
        {
            sn.gameObject.SetActive(true);
        }

        _minigamePassed = false;
        _snoflakesTouched = 0;
    }
    private IEnumerator MinigameStart()
    {
        Debug.Log("minigame started");
        for (float t = 0; t < _jumpTime; t += Time.deltaTime)
        {
            if (_minigamePassed)
            {
                break;
            }
            yield return null;
        }

        _EndMinigameEvent.minigamePassed = _minigamePassed;
        _EndMinigameEvent.Invoke();

        foreach (HardJumpSnowflake sn in _snowflakes)
        {
            Destroy(sn.gameObject);
        }
        Destroy(gameObject);
    }

   


}
