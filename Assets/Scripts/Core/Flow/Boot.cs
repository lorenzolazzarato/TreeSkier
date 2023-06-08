using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boot : Unit {

    [DoNotSerialize]
    public ControlInput InputTrigger;
    [DoNotSerialize]
    public ControlOutput OutputTrigger;

    protected override void Definition() {
        InputTrigger = ControlInput("", InternalBoot);
        OutputTrigger = ControlOutput("");
    }

    private ControlOutput InternalBoot(Flow arg) {
        SystemCoordinator.Instance.StartSystemSetup();
        return OutputTrigger;
    }
}
