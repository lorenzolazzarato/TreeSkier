using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private PauseViewController _pauseViewPrefab;

    public void PauseGame() {
        // TODO: DISABLE PLAYER 
        Time.timeScale = 0;
        Instantiate(_pauseViewPrefab);
        
    }
}
