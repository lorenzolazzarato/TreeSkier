using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour, IMoovableObject
{

    private int _speed = 2;

    public int Speed { get => _speed; }

    //Setup
    public virtual void Setup() { }

    //Clear
    public virtual void Clear() { }
}
