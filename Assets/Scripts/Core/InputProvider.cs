using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    public delegate void OnVector2Delegate(Vector2 value);
    public delegate void OnFloatDelegate(float value);
    public delegate void OnVoidDelegate();
    public delegate void OnBoolDelegate(bool value);
    public delegate void OnTouchDelegate(Vector2 value, float time);

    [Header("Input Provider")]
    [SerializeField]
    protected IdContainer _Id;

    public IdContainer Id => _Id;
}
