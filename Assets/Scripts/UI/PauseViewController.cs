using UnityEngine;

public class PauseViewController : MonoBehaviour {

    [SerializeField]
    private OptionsViewController _optionsViewPrefab;
    public void ResumeGame() {
        FlowSystem.Instance.TriggerFSMEvent("PAUSE_END");
    }

    public void OpenOptions() {
        Instantiate(_optionsViewPrefab);
    }

    public void QuitGame() {
        //TravelSystem.Instance.SceneLoad("MainMenu");
        FlowSystem.Instance.TriggerFSMEvent("TO_MAINMENU");
    }

    
}
