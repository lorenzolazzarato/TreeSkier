using UnityEngine;

public class PauseViewController : MonoBehaviour {

    [SerializeField]
    private OptionsViewController _optionsViewPrefab;

    [SerializeField]
    private IdContainer _ButtonId;

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

    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySoundEffect(_ButtonId);
    }
}
