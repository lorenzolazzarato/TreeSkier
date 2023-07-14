using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private GameOverViewController _pauseViewPrefab;

    private GameOverViewController _pauseObject;

    public void PauseGame() {
        FlowSystem.Instance.TriggerFSMEvent("PAUSE_START");
    }
}
