using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableObject : MovableObject
{
    [SerializeField]
    protected IdContainerGameEvent _HitEvent;

    public override void HitObject()
    {
        _HitEvent.Invoke();
    }
}
