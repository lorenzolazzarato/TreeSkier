using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesSpawner : Spawner
{
    [SerializeField]
    public IdContainer _TreeIdContainer2;

    [SerializeField]
    public IdContainer _TreeIdContainer3;

    protected PoolManager _poolManagerTree2;

    protected PoolManager _poolManagerTree3;


    public override MoovableObject Spawn()
    {
        //Debug.Log("spawned tree from tree spawner");
        MoovableObject spawned = null;
        switch (UnityEngine.Random.Range(1, 4))
        {
            case 1:
                spawned = _poolManager.GetPoolableObject<MoovableObject>();
                break;


            case 2:
                spawned = _poolManagerTree2.GetPoolableObject<MoovableObject>();
                break;


            case 3:
                spawned = _poolManagerTree3.GetPoolableObject<MoovableObject>();
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

        _poolManagerTree2 = PoolingSystem.Instance.getPoolManagerInstance(_TreeIdContainer2);

        _poolManagerTree3 = PoolingSystem.Instance.getPoolManagerInstance(_TreeIdContainer3);


    }
    public override bool SpawnWithPosition(Vector3 position)
    {
        return base.SpawnWithPosition(position);
    }

    public override bool SpawnWithPosition(float x, float y = -1, float z = -1)
    {
        return base.SpawnWithPosition(x, y, z);
    }
}
