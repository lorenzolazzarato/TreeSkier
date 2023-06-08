using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : Singleton<UISystem>, ISystem
{
    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    [SerializeField]
    private GameObject _spawnPoint;

    [SerializeField]
    private IdContainerGameEvent _ViewChangedStateEvent;
    public IdContainerGameEvent ViewChangedStateEvent => _ViewChangedStateEvent;

    private Dictionary<string, ViewController> _viewControllerDictionary;

    public void Setup() {
        _viewControllerDictionary = new Dictionary<string, ViewController>();
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    public ViewController ShowView(IdContainer id, ViewController controller) {
        if (_viewControllerDictionary.ContainsKey(id.Id)) {
            Debug.LogError("View già instanziata con id: "+ id.Id);
            return null;
        }

        ViewController newController = Instantiate(controller, _spawnPoint.transform);
        _viewControllerDictionary.Add(id.Id, newController);

        newController.setup(id);
        newController.State = ViewController.ViewState.Showing;
        return null;
    }

    public void HideView(IdContainer id) {
        if (!_viewControllerDictionary.ContainsKey(id.Id)) {
            Debug.LogError("View non instanziata con id: " + id.Id);
            return;
        }
        ViewController controller = _viewControllerDictionary[id.Id];
        controller.State = ViewController.ViewState.Hiding;
        StartCoroutine(waitForViewHidden(controller));
    }

    private IEnumerator waitForViewHidden(ViewController controller) {
        yield return new WaitUntil(() => { return controller.State == ViewController.ViewState.Hidden; });
        _viewControllerDictionary.Remove(controller.IdContainer.Id);
        Destroy(controller.gameObject);
    }

}
