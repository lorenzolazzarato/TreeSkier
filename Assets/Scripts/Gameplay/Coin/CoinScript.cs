using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : GatherableObject
{

    [SerializeField]
    private int _Score;

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
        ScoreManager.Instance.AddScore(_Score);
        AudioManager.Instance.PlaySoundEffect(_ObjectIdContainer);
    }
}
