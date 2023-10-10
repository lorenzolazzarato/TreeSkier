using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpEndEvent", menuName = "Event/JumpEndEvent", order = 1)]

public class JumpEndEvent : IdContainerGameEvent
{
    public bool IsMinigame = false;
    public bool MinigamePassed = false;
}
