using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Audio sources")]
    [SerializeField]
    private AudioSource _BGMSource;
    [SerializeField]
    private AudioSource _EffectsSource;

    [Header("Audio clips")]
    [SerializeField]
    private AudioClip _CoinSound;
    [SerializeField]
    private AudioClip _BombSound;
    [SerializeField]
    private AudioClip _TreeSound;

    private bool _IsBGMPlaying = true;
    private bool _AreEffectsPlaying = true;

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

    public void ToggleEffects() {
        _EffectsSource.mute = !_EffectsSource.mute;
        _AreEffectsPlaying = !_AreEffectsPlaying;
    }

    public bool IsBGMPlaying() {
        return _IsBGMPlaying;
    }

    public bool AreEffectsPlaying() {
        return _AreEffectsPlaying;
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
