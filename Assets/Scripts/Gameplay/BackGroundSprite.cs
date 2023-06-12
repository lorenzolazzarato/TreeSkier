using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSprite : PoolableObject
{

    [SerializeField]
    private IdContainerGameEvent _OutOfScreenEvent;

    private float height, width;


    // Update is called once per frame
    void Update()
    {

        // If the sprite is active and enabled, move towards the top of the screen
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
            //Debug.Log("Debugging");
        }

        // TODO
        // Checks if the sprite is out of the screen. In case it is, call an event
        if (transform.position.y >= height * 2)
        {
            _OutOfScreenEvent.Invoke();
        }

    }

    public override void Setup()
    {
        base.Setup();

        width = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x;
        height = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y;
    }
}
