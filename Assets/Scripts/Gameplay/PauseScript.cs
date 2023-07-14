using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private PauseViewController _pauseViewPrefab;

    private PauseViewController _pauseObject;

    public void PauseGame() {
        FlowSystem.Instance.TriggerFSMEvent("PAUSE_START");
    }
}
