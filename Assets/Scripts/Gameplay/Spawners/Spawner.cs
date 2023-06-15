using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    public GameObject _StartingSpawnLocation;

    [SerializeField]
    public IdContainer _SpriteIdContainer;


    protected PoolManager _poolManager;


    private void Awake()
    {
        // Get the pool manager instance
        //_poolManager = PoolingSystem.Instance.getPoolManagerInstance(_SpriteIdContainer);

    }

    [ContextMenu("Spawn")]
    public MoovableObject Spawn()
    {
        MoovableObject spawned = _poolManager.GetPoolableObject<MoovableObject>();

        spawned.transform.position = _StartingSpawnLocation.transform.position;

        Debug.LogFormat("Spawned Object: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);

        return spawned;
    }

    public void SpawnWithPosition(Vector3 position)
    {
        MoovableObject spawned = Spawn();

        spawned.transform.position = position;

        Debug.LogFormat("Spawned Object moved to: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);
    }

    public void SpawnWithPosition(float x, float y = -1, float z = -1)
    {
        MoovableObject spawned = Spawn();

        float tempX, tempY, tempZ;

        tempX = x;
        tempY  = y == -1 ? spawned.transform.position.y : y;
        tempZ = z == -1 ? spawned.transform.position.z : z;

        Vector3 position = new Vector3(tempX, tempY, tempZ);

        spawned.transform.position = position;

        Debug.LogFormat("Spawned Object moved to: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);
    }

    public void InitiateSpawner()
    {
        _poolManager = PoolingSystem.Instance.getPoolManagerInstance(_SpriteIdContainer);
    }

}
