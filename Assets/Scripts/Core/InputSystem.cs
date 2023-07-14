using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>, ISystem
{

    

    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    [SerializeField]
    private List<InputProvider> _ProviderList;

    [Header("Events")]
    [SerializeField]
    private IdContainerGameEvent _PauseEnable;

    [SerializeField]
    private IdContainerGameEvent _PauseDisable;

    [SerializeField]
    private IdContainerGameEvent _GameOver;

    [SerializeField]
    private IdContainerGameEvent _ExitGame;

    private Dictionary<string, InputProvider> _providerDictionary;

    public void EnablePause(GameEvent evt) {
        DisableInputProvider("Gameplay");
    }

    public void DisablePause(GameEvent evt) {
        EnableInputProvider("Gameplay");
    }

    public T GetInput<T>(string id) where T : InputProvider
    {
        if (!_providerDictionary.ContainsKey(id))
        {
            return null;
        }

        return  _providerDictionary[id] as T;
    }

    public void EnableInputProvider(string id)
    {
        if (!_providerDictionary.ContainsKey(id))
        {
            return;
        }

        _providerDictionary[id].gameObject.SetActive(true);
    }
    public void DisableInputProvider(string id)
    {
        if (!_providerDictionary.ContainsKey(id))
        {
            return;
        }

        _providerDictionary[id].gameObject.SetActive(false);
    }

    public void Setup() {

        _providerDictionary = new Dictionary<string, InputProvider>();

        foreach (InputProvider provider in _ProviderList) {
            _providerDictionary.Add(provider.Id.Id, provider);
        }
        _PauseEnable.Subscribe(EnablePause);
        _PauseDisable.Subscribe(DisablePause);
        _GameOver.Subscribe(EnablePause);
        _ExitGame.Subscribe(DisablePause);
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

}
