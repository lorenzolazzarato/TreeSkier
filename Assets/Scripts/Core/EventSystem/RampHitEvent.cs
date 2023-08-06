using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RampHitEvent", menuName = "Event/RampHitEvent", order = 1)]
public class RampHitEvent : IdContainerGameEvent
{
    public int difficulty = 0;
}
