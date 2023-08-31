using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBase : MonoBehaviour
{
    [SerializeField]
    private Slider _SliderBar;

    [SerializeField]
    private BaseJumpScriptable _JumpInfo;


    private float _jumpTime;

    private void OnEnable()
    {
        
    }

    public void InitBaseJump(float jumpTime)
    {
        _jumpTime = jumpTime;
        StartCoroutine(SliderReduction());
    }

    private IEnumerator SliderReduction()
    {
        for (float t = 0; t < _jumpTime; t += Time.deltaTime)
        {
            _SliderBar.value = Mathf.Lerp(1, 0, t/_jumpTime);
            yield return null;
        }
    }
}
