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

    [Header("Audio volume")]
    [SerializeField]
    [Range(0f, 1f)]
    private float _BGMSourceVolume;
    [SerializeField]
    [Range(0f, 1f)]
    private float _EffectsSourceVolume;

    [Header("Audio clips")]
    [SerializeField]
    private AudioClip _CoinSound;
    [SerializeField]
    private AudioClip _HeartDropSound;
    [SerializeField]
    private AudioClip _EnemySkierSound;
    [SerializeField]
    private AudioClip _FlagSound;
    [SerializeField]
    private AudioClip _RampHitSound1;
    [SerializeField]
    private AudioClip _RampHitSound2;
    [SerializeField]
    private AudioClip _DamageSound1;
    [SerializeField]
    private AudioClip _DamageSound2;
    [SerializeField]
    private AudioClip _DamageSound3;
    [SerializeField]
    private AudioClip _BombSound1;
    [SerializeField]
    private AudioClip _BombSound2;

    [Header("UI Audio clips")]
    [SerializeField]
    private AudioClip _ButtonSound;
    [SerializeField]
    private AudioClip _ButtonBackSound;

    private bool _IsBGMPlaying = true;
    private bool _AreEffectsPlaying = true;

    private int _DamageSourceNumber = 1;
    private int _BombSourceNumber = 1;
    private int _RampHitSourceNumber = 1;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _BGMSource.volume = _BGMSourceVolume;
        _EffectsSource.volume = _EffectsSourceVolume;
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
                switch (_BombSourceNumber)
                {
                    case 1:
                        _EffectsSource.PlayOneShot(_BombSound1);
                        _BombSourceNumber = 2;
                        break;
                    case 2:
                        _EffectsSource.PlayOneShot(_BombSound2);
                        _BombSourceNumber = 1;
                        break;
                }
                break;
            case "coin":
                _EffectsSource.PlayOneShot(_CoinSound);
                break;
            case "tree1": case "tree2": case "tree3":
            case "FallenTree1":
            case "FallenTree2":
            case "FallenTree3":
                switch(_DamageSourceNumber)
                {
                    case 1:
                        _EffectsSource.PlayOneShot(_DamageSound1);
                        _DamageSourceNumber = 2;
                        break;
                    case 2:
                        _EffectsSource.PlayOneShot(_DamageSound2);
                        _DamageSourceNumber = 3;
                        break;
                    case 3:
                        _EffectsSource.PlayOneShot(_DamageSound3);
                        _DamageSourceNumber = 1;
                        break;
                }
                break;
            case "enemyskier":
                _EffectsSource.PlayOneShot(_EnemySkierSound);
                break;
            case "flag":
                _EffectsSource.PlayOneShot(_FlagSound);
                break;
            case "heartdrop":
                _EffectsSource.PlayOneShot(_HeartDropSound);
                break;
            case "easyRamp":
            case "mediumRamp":
            case "hardRamp":
                switch (_RampHitSourceNumber)
                {
                    case 1:
                        _EffectsSource.PlayOneShot(_RampHitSound1);
                        _RampHitSourceNumber = 2;
                        break;
                    case 2:
                        _EffectsSource.PlayOneShot(_RampHitSound2);
                        _RampHitSourceNumber = 1;
                        break;
                }
                break;
            case "button":
                _EffectsSource.PlayOneShot(_ButtonSound);
                break;
            case "buttonback":
                _EffectsSource.PlayOneShot(_ButtonBackSound);
                break;
        }
    }
}
