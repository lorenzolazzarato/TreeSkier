using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpawner : Spawner
{
    [SerializeField]
    public IdContainer _RampMediumIdContainer;

    [SerializeField]
    public IdContainer _RampHardIdContainer;

    protected PoolManager _poolManagerMediumRamp;

    protected PoolManager _poolManagerHardRamp;

    public override MovableObject Spawn()
    {
        MovableObject spawned = null;
        switch (UnityEngine.Random.Range(1, 4))
        {
            case 1:
                spawned = _poolManager.GetPoolableObject<MovableObject>();
                break;


            case 2:
                spawned = _poolManagerMediumRamp.GetPoolableObject<MovableObject>();
                break;


            case 3:
                spawned = _poolManagerHardRamp.GetPoolableObject<MovableObject>();
                break;

            default:
                break;

        }

        if (spawned != null)
        {
            spawned.transform.position = _StartingSpawnLocation.transform.position;
        }

        //Debug.LogFormat("Spawned Object: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);

        return spawned;
    }

    public override void InitiateSpawner()
    {
        base.InitiateSpawner();

        _poolManagerMediumRamp = PoolingSystem.Instance.getPoolManagerInstance(_RampMediumIdContainer);

        _poolManagerHardRamp = PoolingSystem.Instance.getPoolManagerInstance(_RampHardIdContainer);
    }
}
