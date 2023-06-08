using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstatiatePool : Unit {
    [DoNotSerialize]
    public ControlInput InputTrigger;
    [DoNotSerialize]
    public ControlOutput OutputTrigger;

    [DoNotSerialize]
    public ValueInput SceneToLoadPool;

    protected override void Definition() {
        InputTrigger = ControlInput("", InternalInstantiationPool);
        OutputTrigger = ControlOutput("");
        SceneToLoadPool = ValueInput<string>("Scene To Load Pool", "");
    }

    private ControlOutput InternalInstantiationPool(Flow arg) {
        PoolingSystem.Instance.SceneManagerSetup(arg.GetValue<string>(SceneToLoadPool));
        return OutputTrigger;
    }
}
