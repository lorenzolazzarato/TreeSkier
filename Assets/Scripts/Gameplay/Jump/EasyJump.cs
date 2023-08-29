using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyJump : MonoBehaviour
{
    [SerializeField]
    private EasyJumpScriptable _EasyJumpInfo;

    [SerializeField]
    private BaseJumpScriptable _BaseJumpInfo;

    [SerializeField]
    private EasyJumpCircleDrawer _TargetCircle;

    [SerializeField]
    private EasyJumpCircleDrawer _MoovingCircle;

    [SerializeField]
    private EndMinigameEvent _EndMinigameEvent;

    [SerializeField]
    private float _MoovingStartingRadius = 3;

    private float _jumpTime;

    private float _minAcceptanceTime;
    private float _maxAcceptanceTime;


    private bool _minigamePassed = false;

    private bool _hasTouched = false;

    private void OnEnable()
    {
        _jumpTime = _BaseJumpInfo.TimeForJump;

        _minAcceptanceTime = _EasyJumpInfo._EasyJumpMinAcceptanceTime;
        _maxAcceptanceTime = _EasyJumpInfo._EasyJumpMaxAcceptanceTime;

        // Some nasty calculation to get the correct width of the target ring
        float minW = Mathf.Lerp(_MoovingStartingRadius, 0, _minAcceptanceTime / _jumpTime);
        float maxW = Mathf.Lerp(_MoovingStartingRadius, 0, _maxAcceptanceTime / _jumpTime);

        _TargetCircle.radius = Mathf.Lerp(_MoovingStartingRadius, 0, ((_maxAcceptanceTime + _minAcceptanceTime) / 2) / _jumpTime);
        _TargetCircle.circleRenderer.startWidth = minW - maxW;
        _TargetCircle.circleRenderer.endWidth = minW - maxW;
        //float tempWidth = _TargetCircle.circleRenderer.widthMultiplier;
        //_TargetCircle.circleRenderer.widthMultiplier = tempWidth * (_maxAcceptanceTime / _minAcceptanceTime);
        //Debug.Log(_TargetCircle.circleRenderer.widthMultiplier);

        //_MoovingCircle.radius = Mathf.Lerp(_MoovingStartingRadius - _MoovingCircle.circleRenderer.startWidth, 0, _minAcceptanceTime / _jumpTime);



        _MoovingCircle.radius = _MoovingStartingRadius;

        _TargetCircle.Draw();
        _MoovingCircle.Draw();

        StartCoroutine(MinigameStart());
    }

    private IEnumerator MinigameStart()
    {
        for (float t = 0; t < _jumpTime; t += Time.deltaTime)
        {

            if (_hasTouched)
            {
                _minigamePassed = t > _minAcceptanceTime && t < _maxAcceptanceTime;
                //Debug.Log("minigame ended for touch");
                //Debug.Log(_minigamePassed);
                break;
            }
            _MoovingCircle.radius = Mathf.Lerp(_MoovingStartingRadius - _MoovingCircle.circleRenderer.startWidth, 0, t / _jumpTime);
            
            _MoovingCircle.Draw();
            yield return null;
        }

        _EndMinigameEvent.minigamePassed = _minigamePassed;
        _EndMinigameEvent.Invoke();

        Destroy(gameObject);
    }

    public void EndMinigame()
    {

    }

    public void Touched()
    {
        _hasTouched = true;
    }

}
