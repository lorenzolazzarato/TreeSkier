using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour, IMoovableObject
{

    [SerializeField]
    private int _speed = 10;

    public int Speed { get => _speed; }

    //Setup
    public virtual void Setup() { }

    //Clear
    public virtual void Clear() { }
}
