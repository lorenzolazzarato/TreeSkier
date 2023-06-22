using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : GatherableObject
{
    public override void Setup() 
    { 
        base.Setup();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void HitObject() {
        base.HitObject();
        ScoreManager.Instance.SetMultiplier();
        AudioManager.Instance.PlaySoundEffect(_ObjectIdContainer);
    }

}
