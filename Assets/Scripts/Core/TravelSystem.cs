using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TravelSystem : Singleton<TravelSystem>, ISystem
{
    public delegate void OnTravelCompleteDelegate();
    public OnTravelCompleteDelegate TravelComplete;

    [SerializeField]
    private string _InitialScene;
    [SerializeField]
    private string _LoadingScene;

    private string _currentScene;
    private string _targetScene;

    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    public void SceneLoad(string name)
    {
        StartCoroutine(Load(name));
    }

    private IEnumerator Load(string name)
    {
        _targetScene = name;

        AsyncOperation op_loading = SceneManager.LoadSceneAsync(_LoadingScene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => { return op_loading.isDone; });

        AsyncOperation op_current = SceneManager.UnloadSceneAsync(_currentScene);
        yield return new WaitUntil(() => { return op_current.isDone; });

        AsyncOperation op_target = SceneManager.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => { return op_target.isDone; });
        _currentScene= _targetScene;
        _targetScene= string.Empty;

        op_loading = SceneManager.UnloadSceneAsync(_LoadingScene);
        yield return new WaitUntil(() => { return op_loading.isDone; });

        FlowSystem.Instance.SetFSMVariable("SCENE_TO_LOAD", _currentScene);

        TravelComplete?.Invoke();
    }

    public void Setup() {
        _currentScene = SceneManager.GetActiveScene().name;
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }
}
