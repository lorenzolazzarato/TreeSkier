using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimatedViewController : ViewController
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private bool _skipAnimation;

    [SerializeField]
    private float _fadeDuration = 0.3f;

    [SerializeField]
    private float _fadeSteps = 100f;

    protected override void onHiding() {
        if(_skipAnimation) {
            State = ViewState.Hidden; 
            return;
        }
        StartCoroutine(FadeOut());
    }

    protected override void onShowing() {
        if (_skipAnimation) {
            State = ViewState.Shown;
            return;
        }
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        _canvasGroup.alpha = 0f;
        float elapsedTime = 0f;
        float theOne = _fadeDuration / _fadeSteps;
        while (elapsedTime < _fadeDuration) {
            _canvasGroup.alpha = (elapsedTime / _fadeDuration);
            yield return new WaitForSecondsRealtime(theOne);
            elapsedTime += theOne;
        }
        State = ViewState.Shown;
    }

    private IEnumerator FadeOut() {
        _canvasGroup.alpha = 1f;
        float elapsedTime = 0f;
        float theOne = _fadeDuration / _fadeSteps;
        while (elapsedTime < _fadeDuration) {
            _canvasGroup.alpha = 1 - (elapsedTime / _fadeDuration);
            yield return new WaitForSecondsRealtime(theOne);
            elapsedTime += theOne;
        }
        State = ViewState.Hidden;
    }
}
