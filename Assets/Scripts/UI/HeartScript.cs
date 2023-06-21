using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{

    [Header("Sprites")]
    [SerializeField]
    private Sprite _EmptyHeart;
    [SerializeField]
    private Sprite _HalfHeart;
    [SerializeField]
    private Sprite _FullHeart;

    private Image _image;

    void Start() {
        _image = GetComponent<Image>();
    }

    public void ChangeSprite(HeartValue value) {
        switch(value) {
            case HeartValue.Empty:
                _image.sprite = _EmptyHeart;
                break;
            case HeartValue.Half:
                _image.sprite = _HalfHeart;
                break;
            case HeartValue.Full:
                _image.sprite = _FullHeart;
                break;
        }
    }
}

public enum HeartValue {
    Empty = 0, Half = 1, Full = 2
}
