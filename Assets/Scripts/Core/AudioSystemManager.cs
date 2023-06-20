using System.ComponentModel.Design;
using UnityEngine;

public class AudioSystemManager : MonoBehaviour {
    public static AudioSystemManager Instance;

    [SerializeField]
    private AudioSource _BGMSource;

    private bool _IsBGMPlaying = true;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void ToggleBGM() {
        _BGMSource.mute = !_BGMSource.mute;
        _IsBGMPlaying = !_IsBGMPlaying;
    }

    public bool IsBGMPlaying() {
        return _IsBGMPlaying;
    }
}
