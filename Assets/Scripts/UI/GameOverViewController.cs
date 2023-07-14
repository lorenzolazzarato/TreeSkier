using UnityEngine;

public class GameOverViewController : MonoBehaviour {
    public void QuitGame() {
        FlowSystem.Instance.TriggerFSMEvent("TO_MAINMENU");
    }

    
}
