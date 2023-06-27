using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : HittableObject
{
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
    }
}
