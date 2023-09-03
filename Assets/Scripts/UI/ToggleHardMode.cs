using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHardMode : ToggleController
{
    protected override void Awake() {
        base.Awake();
        if (!AudioManager.Instance.IsBGMPlaying()) {
            _toggle.isOn = false;
        }

    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change) {
        //AudioManager.Instance.ToggleEffects();
    }

    public override void OnSwitch(bool isOn) {
        base.OnSwitch(isOn);
        AudioManager.Instance.EnableBGM(isOn);
    }

    protected override void OnDestroy() {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
