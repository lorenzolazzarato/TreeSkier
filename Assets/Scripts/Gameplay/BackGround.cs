using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField]
    public GameObject _StartingSpawnLocation;
    
    [SerializeField]
    public GameObject _BottomSpawnLocation;

    [SerializeField]
    public IdContainer _BackGroundIdContainer;

    [SerializeField]
    private IdContainerGameEvent _OutOfScreenEvent;

    private PoolManager _poolManager;

    private float _spriteHeight;

    // We use three background for safety, two should be enough
    BackGroundSprite _firstBg, _secondBg, _thirdBg;

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
        _firstBg = _poolManager.GetPoolableObject<BackGroundSprite>();
        _secondBg = _poolManager.GetPoolableObject<BackGroundSprite>();
        _thirdBg = _poolManager.GetPoolableObject<BackGroundSprite>();

        _spriteHeight = _firstBg.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;

        // At the start set the position based on the spawn location
        _firstBg.transform.position = _StartingSpawnLocation.transform.position;

        _secondBg.transform.position = new Vector3(_StartingSpawnLocation.transform.position.x, 
            _StartingSpawnLocation.transform.position.y - _spriteHeight, 
            _StartingSpawnLocation.transform.position.z);

        _thirdBg.transform.position = new Vector3(_secondBg.transform.position.x,
            _secondBg.transform.position.y - _spriteHeight,
            _secondBg.transform.position.z);

    }

    private void SpawnAnotherBackground(GameEvent evt)
    {
        //Debug.Log("Have to spawn another background");

        // Return the BG that is out of the screen
        _poolManager.ReturnPoolableObject(_firstBg);

        _firstBg = _secondBg;

        _secondBg = _thirdBg;

        // Create another bg at the bottom of the last one
        _thirdBg = _poolManager.GetPoolableObject<BackGroundSprite>();

        _thirdBg.transform.position = new Vector3(_secondBg.transform.position.x,
            _secondBg.transform.position.y - _spriteHeight,
            _secondBg.transform.position.z);

    }
    
}
