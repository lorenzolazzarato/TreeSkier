using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHardMode : ToggleController
{
    [SerializeField]
    private IdContainer _toggleEventIdContainer;


    protected override void Awake() {
        base.Awake();
        if (!AudioManager.Instance.IsBGMPlaying()) {
            _toggle.isOn = false;
        }

    }
    private void OnEnable()
    {
        if (FlowSystem.Instance.GetFSMVariable<bool>("EasyMode"))
        {
            _toggle.isOn = false;
        }
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change) {
        //AudioManager.Instance.ToggleEffects();
    }

    public override void OnSwitch(bool isOn) {
        base.OnSwitch(isOn);
        //AudioManager.Instance.EnableBGM(isOn);
        FlowSystem.Instance.SetFSMVariable("EasyMode", !FlowSystem.Instance.GetFSMVariable<bool>("EasyMode"));
    }

    protected override void OnDestroy() {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
