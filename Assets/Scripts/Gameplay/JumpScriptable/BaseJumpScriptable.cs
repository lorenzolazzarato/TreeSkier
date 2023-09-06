using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseJumpScriptable", menuName = "JumpScriptable/BaseJumpScriptable", order = 1)]
public class BaseJumpScriptable : ScriptableObject
{
    public float InitialTimeForJump = 5;
    
    public float TimeReductionForEasy = 0.4f;
    public float TimeReductionForMedium = 0.8f;
    public float TimeReductionForHard = 1.2f;

    public float JumpDurationWithoutMinigame = 1f;
}
