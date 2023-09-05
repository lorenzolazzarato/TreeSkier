using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsViewController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _BackButtonId;

    public void CloseOptionsView()
    {
        Destroy(gameObject);
    }

    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySoundEffect(_BackButtonId);
    }

}
