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

    public void EnableBGM(bool value) {
        _BGMSource.mute = !value;
        _IsBGMPlaying = value;
    }

    public void EnableEffects(bool value) {
        _EffectsSource.mute = !value;
        _AreEffectsPlaying = value;
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
            case "tree1": case "tree2": case "tree3":
            case "FallenTree1":
            case "FallenTree2":
            case "FallenTree3":
                _EffectsSource.PlayOneShot(_TreeSound);
                break;
        }
    }
}
