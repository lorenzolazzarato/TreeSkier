using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public enum ViewState {NONE, Showing, Hiding, Hidden, Shown };

    [SerializeField]
    protected IdContainer _IdContainer;

    protected ViewState _State;
    public IdContainer IdContainer => _IdContainer;
    public ViewState State {
        get { return _State; }
        set { 
            _State = value;

            switch (_State) {
                case ViewState.Showing:
                    onShowing();
                    break;

                case ViewState.Hidden: 
                    onHidden(); 
                    break;

                case ViewState.Hiding: 
                    onHiding(); 
                    break;

                case ViewState.Shown: 
                    onShown(); 
                    break;
                default:
                    Debug.LogError("OH FUCK in View Controller");
                    break;
            }
            
            UISystem.Instance.ViewChangedStateEvent.idContainer = _IdContainer;
            UISystem.Instance.ViewChangedStateEvent.Invoke();
        }
    }

    public void setup(IdContainer id) {
        _IdContainer = id;
        OnSetup();
    }
    protected virtual void onShown() { }
    protected virtual void onHidden() { }
    protected virtual void onShowing() { }
    protected virtual void onHiding() { }
    protected virtual void OnSetup() { }
}
