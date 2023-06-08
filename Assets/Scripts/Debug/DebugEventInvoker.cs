using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEventInvoker : MonoBehaviour
{
    [SerializeField]
    private IdContainerGameEvent _DebugEvent;

    [SerializeField]
    private IdContainer _DebugIdContainer;

    [ContextMenu("InvokeEvent")]
    public void InvokeEvent() {
        _DebugEvent.idContainer = _DebugIdContainer;
        _DebugEvent.Invoke();
    }
}
