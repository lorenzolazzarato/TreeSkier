using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : HittableObject
{
    // 0 = nothing
    // 1 = easy
    // 2 = medium
    // 3 = hard
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
        //Debug.Log("colpita rampa");
        RampHitEvent rampHitEvent = (RampHitEvent)_HitEvent;

        if (rampHitEvent != null)
        {
            AudioManager.Instance.PlaySoundEffect(_ObjectIdContainer);
            rampHitEvent.difficulty = _difficulty;
            rampHitEvent.Invoke();
        }

    }
    
}
