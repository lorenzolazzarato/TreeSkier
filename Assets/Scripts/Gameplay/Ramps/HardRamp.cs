using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardRamp : Ramp
{
    public override void Setup()
    {
        base.Setup();
        _difficulty = 3;
    }

    protected override void Update()
    {
        base.Update();
    }

   
}
