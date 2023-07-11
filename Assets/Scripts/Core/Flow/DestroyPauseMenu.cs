using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyPauseMenu : Unit
{
    [DoNotSerialize]
    public ControlInput InputTrigger;
    [DoNotSerialize]
    public ControlOutput OutputTrigger;

    [DoNotSerialize]
    public ValueInput PauseMenuPrefab;

    protected override void Definition()
    {
        InputTrigger = ControlInput("", InstantiatePause);
        OutputTrigger = ControlOutput("");

        PauseMenuPrefab = ValueInput<PauseScript>("PauseMenuPrefab", null);
    }

    private ControlOutput InstantiatePause(Flow arg)
    {
        PauseScript p = arg.GetValue<PauseScript>(PauseMenuPrefab);
        if (p != null)
        {
            p.DestroyPause();
        }
        else
        {
            Debug.Log("Can't destroy");
        }
        return OutputTrigger;
    }
}
