using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EasyJumpScriptable", menuName = "JumpScriptable/EasyJumpScriptable", order = 1)]
public class EasyJumpScriptable : ScriptableObject
{
    public float _EasyJumpMinAcceptanceTime;

    public float _EasyJumpMaxAcceptanceTime;
}
