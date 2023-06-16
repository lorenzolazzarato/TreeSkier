using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableObject : MoovableObject
{
    [SerializeField]
    private IdContainerGameEvent _HitEvent;

    public override void HitObject()
    {
        _HitEvent.Invoke();
    }
}
