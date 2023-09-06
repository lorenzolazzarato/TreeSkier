using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToggleHardMode : ToggleController
{
    [SerializeField]
    private IdContainer _toggleEventIdContainer;


    protected override void Awake() {
        base.Awake();
        Debug.Log("Easy mode: " + FlowSystem.Instance.GetFSMVariable<bool>("EasyMode"));
        if (FlowSystem.Instance.GetFSMVariable<bool>("EasyMode")) {
            _toggle.isOn = false;
        }
        
        if(SceneManager.GetActiveScene().name == "Gameplay") { 
            _toggle.interactable = false;
        } else _toggle.interactable = true;

    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change) {
        //AudioManager.Instance.ToggleEffects();
    }

    public override void OnSwitch(bool isOn) {
        base.OnSwitch(isOn);
        if (isOn) {
            FlowSystem.Instance.SetFSMVariable("EasyMode", false);
        } else {
            FlowSystem.Instance.SetFSMVariable("EasyMode", true);
        }
        
    }

    protected override void OnDestroy() {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
