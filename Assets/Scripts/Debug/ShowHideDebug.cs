using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideDebug : MonoBehaviour
{
    public ViewController DebugView;
    public IdContainer DebugViedId;

    [ContextMenu("ShowDebugView")]
    public void ShowDebugView() {
        UISystem.Instance.ShowView(DebugViedId, DebugView);
    }

    [ContextMenu("HideDebugView")]
    public void HideDebugView() {
        UISystem.Instance.HideView(DebugViedId);
    }
}
