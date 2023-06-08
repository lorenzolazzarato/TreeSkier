using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEventListener : MonoBehaviour
{
    [SerializeField]
    private IdContainerGameEvent _DebugEvent;

    private void OnEnable() {
        _DebugEvent.Subscribe(DebugCallback);
    }
    private void DebugCallback(GameEvent evt) {
        IdContainerGameEvent gameEvent = (IdContainerGameEvent)evt;
        Debug.Log(gameEvent.idContainer.Id);
    }

}
