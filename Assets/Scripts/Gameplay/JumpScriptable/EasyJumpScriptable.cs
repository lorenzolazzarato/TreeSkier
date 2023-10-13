using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EasyJumpScriptable", menuName = "JumpScriptable/EasyJumpScriptable", order = 1)]
public class EasyJumpScriptable : ScriptableObject
{
    public float _EasyJumpMinAcceptanceTimeEasy;
    public float _EasyJumpMaxAcceptanceTimeEasy;

    public float _EasyJumpMinAcceptanceTimeMedium;
    public float _EasyJumpMaxAcceptanceTimeMedium;

    public float _EasyJumpMinAcceptanceTimeHard;
    public float _EasyJumpMaxAcceptanceTimeHard;
}
