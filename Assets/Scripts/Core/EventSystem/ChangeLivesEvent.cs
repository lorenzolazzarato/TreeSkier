using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeLivesEvent", menuName = "Event/ChangeLivesEvent", order = 1)]
public class ChangeLivesEvent : IdContainerGameEvent
{
    public int numberOfLives = 0;
}
