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
    public virtual MovableObject Spawn()
    {
        //Debug.LogFormat("Spawned {0} from normal spawn", _SpriteIdContainer);

        MovableObject spawned = _poolManager.GetPoolableObject<MovableObject>();

        if (spawned != null)
        {
            spawned.transform.position = _StartingSpawnLocation.transform.position;
        }

        //Debug.LogFormat("Spawned Object: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);

        return spawned;
    }

    public virtual bool SpawnWithPosition(Vector3 position)
    {
        MovableObject spawned = Spawn();

        if (spawned == null) 
        {
            return false;
        }

        spawned.transform.position = position;
        return true;

        //Debug.LogFormat("Spawned Object moved to: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);
    }

    public virtual bool SpawnWithPosition(float x, float y = -1, float z = -1)
    {
        
        MovableObject spawned = Spawn();

        if (spawned == null)
        {
            return false;
        }

        float tempX, tempY, tempZ;

        tempX = x;
        tempY = y == -1 ? spawned.transform.position.y : y;
        tempZ = z == -1 ? spawned.transform.position.z : z;

        Vector3 position = new Vector3(tempX, tempY, tempZ);

        spawned.transform.position = position;

        //Debug.LogFormat("Spawned Object moved to: {0}, {1}, {2}", spawned.transform.position.x, spawned.transform.position.y, spawned.transform.position.z);
        
        return true;

    }

    public virtual void InitiateSpawner()
    {
        _poolManager = PoolingSystem.Instance.getPoolManagerInstance(_SpriteIdContainer);
    }

}
