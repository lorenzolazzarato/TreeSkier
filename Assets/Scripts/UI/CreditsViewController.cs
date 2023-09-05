using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsViewController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _BackButtonId;

    public void CloseCreditsView()
    {
        Destroy(gameObject);
    }

    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySoundEffect(_BackButtonId);
    }
}
