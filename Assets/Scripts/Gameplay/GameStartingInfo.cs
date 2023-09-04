using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStartingInfo", menuName = "GameStartingInfo/GameStartingInfo", order = 1)]
public class GameStartingInfo : ScriptableObject
{
    public float startingSpeed = 2f;
    public float speedRatio = 1f;
    public bool easyMode = true;
}
