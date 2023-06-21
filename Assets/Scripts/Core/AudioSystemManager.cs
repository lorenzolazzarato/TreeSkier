using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class AudioSystemManager : MonoBehaviour {
    public static AudioSystemManager Instance;

    [SerializeField]
    private AudioSource _BGMSource;
    [SerializeField]
    private AudioSource _EffectsSource;

    [SerializeField]
    private AudioClip _CoinSound;
    [SerializeField]
    private AudioClip _BombSound;
    [SerializeField]
    private AudioClip _TreeSound;

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

    public void PlaySoundEffect(IdContainer id) {
        switch(id.Id) {
            case "bomb":
                _EffectsSource.PlayOneShot(_BombSound);
                break;
            case "coin":
                _EffectsSource.PlayOneShot(_CoinSound);
                break;
        }
    }
}
