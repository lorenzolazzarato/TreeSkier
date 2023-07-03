using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : HittableObject
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    public override void Setup()
    {
        base.Setup();
    }

    public override void HitObject() {
        base.HitObject();
        AudioManager.Instance.PlaySoundEffect(_ObjectIdContainer);
    }
}
