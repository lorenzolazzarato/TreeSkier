using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratePauseMenu : Unit
{

    [DoNotSerialize]
    public ControlInput InputTrigger;
    [DoNotSerialize]
    public ControlOutput OutputTrigger;

    [DoNotSerialize]
    public ValueInput PauseMenu;

    protected override void Definition()
    {
        InputTrigger = ControlInput("", InstantiatePause);
        OutputTrigger = ControlOutput("");

        PauseMenu = ValueInput<PauseScript>("PauseMenu", null);
    }

    private ControlOutput InstantiatePause(Flow arg)
    {
        PauseScript p = arg.GetValue<PauseScript>(PauseMenu);
        if (p != null)
        {
            p.InstantiatePauseView();
        }
        else
        {
            Debug.Log("Can't instantiate");
        }
        return OutputTrigger;
    }
}
