using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDropScript : GatherableObject {

    [SerializeField]
    private IdContainerGameEvent _LifeUpEvent;
    public override void Setup() {
        base.Setup();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void HitObject() {
        base.HitObject();
        _LifeUpEvent.Invoke();
        AudioManager.Instance.PlaySoundEffect(_ObjectIdContainer);
    }
}
