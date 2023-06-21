using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnersList", menuName = "ScriptableObjects/SpawnersList", order = 1)]
public class SpawnersList : ScriptableObject
{
    public List<SpawnerWithProbability> SpawnerList;
}
