using UnityEngine;

public class PauseViewController : MonoBehaviour {

    [SerializeField]
    private OptionsViewController _optionsViewPrefab;
    public void ResumeGame() {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void OpenOptions() {
        Instantiate(_optionsViewPrefab);
    }

    public void QuitGame() {
        TravelSystem.Instance.SceneLoad("MainMenu");
    }
}
