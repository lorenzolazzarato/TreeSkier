using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class SpawnerWithProbability
{
    public Spawner SpawnerObject;
    public int Probability;
}

public class SpawnerManager : MonoBehaviour
{

    [SerializeField]
    private SpawnersList _Spawners;

    [SerializeField]
    private int _MinTimeForSpawn;

    [SerializeField]
    private int _MaxTimeForSpawn;

    // If the spawner manager can spawn the next object
    private bool _canSpawn = true;

    private int totalProbability;
    private int totalSpawners;

    private void Awake()
    {
        totalProbability = 0;
        foreach (SpawnerWithProbability spawner in _Spawners.SpawnerList)
        {
            // Calculate the total probability
            totalProbability += spawner.Probability;

            // Also initiate each spawner
            spawner.SpawnerObject.InitiateSpawner();
        }

        totalSpawners = _Spawners.SpawnerList.Count;
    }
    private void Update()
    {
        if (_canSpawn)
        {
            _canSpawn = false;
            SpawnSomething();
            StartCoroutine(SpawnCoolDown());
        }

    }

    // Starts a cooldown for the next spawn
    private IEnumerator SpawnCoolDown()
    {
        float waitTime = UnityEngine.Random.Range(_MinTimeForSpawn, _MaxTimeForSpawn);
        yield return new WaitForSeconds(waitTime);
        _canSpawn = true;
    }


    private void SpawnSomething()
    {
        int value = UnityEngine.Random.Range(0, totalProbability);

        Spawner spawner = GetCorrectSpawner(value);

        spawner.SpawnWithPosition(UnityEngine.Random.Range(-4f, 4f));

        //Debug.Log("Spawned something");

    }

    // Gets the correct spawner selected from the random value passed (used when choosing what the game has to spawn)
    private Spawner GetCorrectSpawner(int value)
    {
        int currValue = 0;


        foreach (SpawnerWithProbability spawner in _Spawners.SpawnerList)
        {
            // Considering [min, max)
            if (value >= currValue && value < currValue + spawner.Probability)
            {
                return spawner.SpawnerObject;
            }
            else
            {
                currValue += spawner.Probability;
            }
        }


        return _Spawners.SpawnerList[0].SpawnerObject;
    }
}
