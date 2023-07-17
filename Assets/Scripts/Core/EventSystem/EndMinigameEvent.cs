using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndMinigameEvent", menuName = "Event/EndMinigameEvent", order = 1)]
public class EndMinigameEvent : IdContainerGameEvent
{
    public bool minigamePassed = false;
}
