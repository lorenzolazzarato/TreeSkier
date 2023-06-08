using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemCoordinator : Singleton<SystemCoordinator>
{
    private List<ISystem> _ISystemList;

    private Dictionary<ISystem, bool> _dictSystems;
    private int _counterSystemReady = 0;

    [SerializeField]
    private string _OnAllSystemReadyFSMSetup;

    private void Start() {
        StartSystemSetup();
    }
    public void StartSystemSetup() {
        List<GameObject> objects = FindObjectsOfType<GameObject>().ToList();
        _ISystemList = new List<ISystem>();
        foreach (GameObject obj in objects) {
            ISystem iSystem = obj.GetComponent<ISystem>();
            if(iSystem != null) {
                _ISystemList.Add(iSystem);
            }
        }

        _ISystemList = _ISystemList.OrderByDescending(P => P.Priority).ToList();

        _dictSystems = new Dictionary<ISystem, bool>();

        foreach (ISystem sys in _ISystemList) {
            //Debug.LogFormat("System added: {0}", sys.ToString());
            _dictSystems.Add(sys, false);
            sys.Setup();
        }
    }

    public void FinishSystemSetup(ISystem sys) {
        if (!_dictSystems.ContainsKey(sys)) {
            Debug.LogError("FUCK OFF");
            return;
        }
        _dictSystems[sys] = true;
        ++_counterSystemReady;
        CheckAllSystemsReady();
    }

    public void CheckAllSystemsReady() {
        if(_counterSystemReady == _ISystemList.Count) {
            FlowSystem.Instance.TriggerFSMEvent(_OnAllSystemReadyFSMSetup);
        }
    }
}
