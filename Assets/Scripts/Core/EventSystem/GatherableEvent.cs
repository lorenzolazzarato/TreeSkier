using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GatherableEvent", menuName = "Event/GatherableEvent", order = 1)]
public class GatherableEvent : IdContainerGameEvent
{
    public GatherableObject gatheredObject = null;
}
