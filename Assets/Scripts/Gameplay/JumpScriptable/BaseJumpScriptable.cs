using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseJumpScriptable", menuName = "JumpScriptable/BaseJumpScriptable", order = 1)]
public class BaseJumpScriptable : ScriptableObject
{
    public float _InitialTimeForJump = 5;
    
    public float _TimeForJump = 5;

    public float _TimeReductionForDifficulty = 0.4f;
}
