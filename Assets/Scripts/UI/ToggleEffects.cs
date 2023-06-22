//Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class ToggleEffects : MonoBehaviour {
    Toggle m_Toggle;

    void Awake() {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        if (!AudioManager.Instance.AreEffectsPlaying()) {
            m_Toggle.isOn = false;
        }
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change) {
        AudioManager.Instance.ToggleEffects();
    }
}