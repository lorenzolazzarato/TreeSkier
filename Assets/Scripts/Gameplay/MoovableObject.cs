using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovableObject : PoolableObject
{

    protected float height, width;

    protected virtual void Update()
    {
        Move();
    }

    // Move to the top of the screen
    protected void Move()
    {
        // If the sprite is active and enabled, move towards the top of the screen
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
            //Debug.Log("Debugging");
        }
    }

    public override void Setup()
    {
        base.Setup();

        width = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x;
        height = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;
    }
}
