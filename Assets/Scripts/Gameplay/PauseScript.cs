using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public void PauseGame() {
        FlowSystem.Instance.TriggerFSMEvent("PAUSE_START");
    }
}
