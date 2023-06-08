using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolManagerBinding {
    //Model
    public string SceneName;
    public List<PoolManager> poolManagersList;
}


public class PoolingSystem : Singleton<PoolingSystem>, ISystem {

    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    [SerializeField]
    private PoolingSystemData _PoolingSystemData;
    // string: scene name
    private Dictionary<string, List<PoolManager>> _poolManagersDictionary;

    private Dictionary<string, PoolManager> _CurrentManagersDictionary;

    public void Setup() {
        _poolManagersDictionary = new Dictionary<string, List<PoolManager>>();
        _CurrentManagersDictionary = new Dictionary<string, PoolManager>();
        foreach (PoolManagerBinding binding in _PoolingSystemData.PoolManagerBindings)
        {
            _poolManagersDictionary.Add(binding.SceneName, new List<PoolManager>(binding.poolManagersList));
        }
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    public void SceneManagerSetup(string sceneName) {
        if (!_poolManagersDictionary.ContainsKey(sceneName)) return;
        if(_CurrentManagersDictionary.Count > 0) { DestroyAllManagers(); }
        List<PoolManager> _list = _poolManagersDictionary[sceneName];
        foreach (PoolManager poolManager in _list) {
            PoolManager _currentManager = Instantiate(poolManager, gameObject.transform);
            _currentManager.Setup();
            _CurrentManagersDictionary.Add(_currentManager.ID, _currentManager);
        }
    }

    public void DestroyAllManagers() {
        foreach(string _id in _CurrentManagersDictionary.Keys) {
            Destroy(_CurrentManagersDictionary[_id].gameObject);
        }
        _CurrentManagersDictionary.Clear();
    }

    public PoolManager getPoolManagerInstance(IdContainer idContainer) {
        if (!_CurrentManagersDictionary.ContainsKey(idContainer.Id)) {
            Debug.LogError("Error: Pooling manager not found with id: " + idContainer.Id);
            return null;
        }
        return _CurrentManagersDictionary[idContainer.Id];
    }
}
