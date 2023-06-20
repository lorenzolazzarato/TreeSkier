using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField]
    private OptionsViewController _OptionsViewPrefab;

    private OptionsViewController _optionsViewController;

    [SerializeField]
    private CreditsViewController _CreditsViewPrefab;

    private CreditsViewController _creditsViewController;

    public void ChangeScene(string scene)
    {
        TravelSystem.Instance.SceneLoad(scene);
    }

    public void OpenOptions()
    {
        if (_optionsViewController) return;
        _optionsViewController = Instantiate(_OptionsViewPrefab);
    }

    public void OpenCredits()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
