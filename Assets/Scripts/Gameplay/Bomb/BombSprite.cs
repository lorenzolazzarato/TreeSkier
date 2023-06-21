using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSprite : GatherableObject
{

    [SerializeField]
    private IdContainerGameEvent _HitEvent;

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
        base.HitObject();
        // Audiomanager.playSound(this.containerId);
        _HitEvent.Invoke();
    }

}
