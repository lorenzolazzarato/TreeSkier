//Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class ToggleEffects : ToggleController {

    protected override void Awake() {
        base.Awake();
        if (!AudioManager.Instance.AreEffectsPlaying()) {
            _toggle.isOn = false;
        }
        
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change) {
        //AudioManager.Instance.ToggleEffects();
    }

    public override void OnSwitch(bool isOn) {
        base.OnSwitch(isOn);
        AudioManager.Instance.EnableEffects(isOn);
    }

    protected override void OnDestroy() {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}