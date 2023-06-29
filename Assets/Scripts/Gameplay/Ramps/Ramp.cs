using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : HittableObject
{
    protected int _difficulty = 0;
    protected override void Update()
    {
        base.Update();
    }

    public override void Setup()
    {
        base.Setup();
    }

    public override void HitObject()
    {
        Debug.Log("colpita rampa");
        RampHitEvent rampHitEvent = (RampHitEvent)_HitEvent;

        if (rampHitEvent != null)
        {
            rampHitEvent.difficulty = _difficulty;
            rampHitEvent.Invoke();
        }

    }
    
}
