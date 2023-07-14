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

    [DoNotSerialize]
    public ValueInput eventP;

    protected override void Definition()
    {
        InputTrigger = ControlInput("", InstantiatePause);
        OutputTrigger = ControlOutput("");

        PauseMenu = ValueInput<PauseScript>("PauseMenu", null);
        eventP = ValueInput<IdContainerGameEvent>("PauseEvent", null);
    }

    private ControlOutput InstantiatePause(Flow arg)
    {
        PauseScript p = arg.GetValue<PauseScript>(PauseMenu);
        IdContainerGameEvent aa = arg.GetValue<IdContainerGameEvent>(eventP);
        if (p != null)
        {
            //p.InstantiatePauseView();
        }
        else
        {
            Debug.Log("Can't instantiate");
        }
        aa.Invoke();

        return OutputTrigger;
    }
}
