using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovableObject : PoolableObject
{

    [SerializeField]
    public IdContainer _ObjectIdContainer;

    [SerializeField]
    public GameObject _FinishPosition;

    protected float height, width;

    protected PoolManager _poolManager;

    protected BoxCollider2D _boxCollider;

    protected virtual void Update()
    {
        Move();
        // Checks if the sprite is out of the screen. In case it is, call an event
        // Use an object to check the Y coordinate
        if (transform.position.y >= _FinishPosition.transform.position.y)
        {
            RemoveSelf();
        }
    }

    // Move to the top of the screen
    protected void Move()
    {
        // If the sprite is active and enabled, move towards the top of the screen
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
        }
    }

    public override void Setup()
    {
        base.Setup();


        width = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x;
        height = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;

        _poolManager = PoolingSystem.Instance.getPoolManagerInstance(_ObjectIdContainer);
        _boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Used to return the object to its pooling manager
    protected virtual void RemoveSelf()
    {
        if (isActiveAndEnabled)
        {
            _poolManager.ReturnPoolableObject(this);
        }
    }

    // Function called when the object gets hit by the player
    public virtual void HitObject()
    {
        RemoveSelf();
    }
}
