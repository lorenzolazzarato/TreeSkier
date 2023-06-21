using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableObject : MovableObject
{
    [SerializeField]
    private GatherableEvent _GatherEvent;

    public override void HitObject()
    {
        base.HitObject();
        _GatherEvent.gatheredObject = _ObjectIdContainer;
        _GatherEvent.Invoke();
    }
}
