using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField]
    public GameObject _StartingSpawnLocation;
    

    [SerializeField]
    public IdContainer _BackGroundIdContainer;

    [SerializeField]
    private IdContainerGameEvent _OutOfScreenEvent;

    private PoolManager _poolManager;

    private float _spriteHeight;

    // We use three background for safety, two should be enough
    BackGroundSprite _bgSprite;
    Queue<BackGroundSprite> _queue;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _OutOfScreenEvent.Subscribe(SpawnAnotherBackground);
    }

    private void OnDisable()
    {
        _OutOfScreenEvent.Unsubscribe(SpawnAnotherBackground);
    }
    void Awake()
    {
        // Get the pool manager instance and initiate the background
        _poolManager = PoolingSystem.Instance.getPoolManagerInstance(_BackGroundIdContainer);

        // Create a queue to handle the background sprite
        _queue = new Queue<BackGroundSprite>();

        _bgSprite = _poolManager.GetPoolableObject<BackGroundSprite>();
        
        _spriteHeight = _bgSprite.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;

        _bgSprite.transform.position = new Vector3(_StartingSpawnLocation.transform.position.x,
        _StartingSpawnLocation.transform.position.y + _spriteHeight * 2,
        _StartingSpawnLocation.transform.position.z);

        _queue.Enqueue(_bgSprite);

        // Create 3 background sprite and move the in the right position
        for (int i = 1; i > -2; --i)
        {
            _bgSprite = _poolManager.GetPoolableObject<BackGroundSprite>();

            _bgSprite.transform.position = new Vector3(_StartingSpawnLocation.transform.position.x,
            _StartingSpawnLocation.transform.position.y + _spriteHeight * i,
            _StartingSpawnLocation.transform.position.z);

            _queue.Enqueue(_bgSprite);

        }

    }

    private void SpawnAnotherBackground(GameEvent evt)
    {
        //Debug.Log("Have to spawn another background");

        // Return the BG that is out of the screen
        _poolManager.ReturnPoolableObject(_queue.Dequeue());

        // Create another bg at the bottom of the last one
        BackGroundSprite bgSprite = _poolManager.GetPoolableObject<BackGroundSprite>();

        bgSprite.transform.position = new Vector3(_bgSprite.transform.position.x,
            _bgSprite.transform.position.y - _spriteHeight,
            _bgSprite.transform.position.z);

        // Enqueue the new sprite just created
        _queue.Enqueue(bgSprite);

        // Keep track of the last sprite create
        _bgSprite = bgSprite;
    }
    
}
