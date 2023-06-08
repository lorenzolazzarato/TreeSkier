using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, ISystem
{
    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    [SerializeField]
    private List<InputProvider> _ProviderList;

    private Dictionary<string, InputProvider> _providerDictionary;

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

        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

}
