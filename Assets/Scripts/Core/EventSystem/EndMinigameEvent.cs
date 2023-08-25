using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndMinigameEvent", menuName = "Event/EndMinigameEvent", order = 1)]
public class EndMinigameEvent : IdContainerGameEvent
{
    [Header("Event called when the minigame ends but the jump is still in progress")]

    public bool minigamePassed = false;

}
