using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseJumpScriptable", menuName = "JumpScriptable/BaseJumpScriptable", order = 1)]
public class BaseJumpScriptable : ScriptableObject
{
    public float InitialTimeForJump = 5;
    
    public float TimeReductionForDifficulty = 0.4f;

    public bool EasyMode = true;

    public float JumpDurationWithoutMinigame = 1f;
}
