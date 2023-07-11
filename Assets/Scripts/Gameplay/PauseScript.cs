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

    public void InstantiatePauseView()
    {
        // TODO: DISABLE PLAYER 
        Time.timeScale = 0;
        _pauseObject = Instantiate(_pauseViewPrefab);
    }

    public void DestroyPause()
    {
        Time.timeScale = 1;
        Destroy(_pauseObject.gameObject);
    }
}
