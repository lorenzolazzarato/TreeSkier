using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSprite : MoovableObject
{

    [SerializeField]
    private IdContainerGameEvent _OutOfScreenEvent;


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Checks if the sprite is out of the screen. In case it is, call an event
        if (transform.position.y >= height * 2)
        {
            _OutOfScreenEvent.Invoke();
        }

    }

    public override void Setup()
    {
        base.Setup();

        
    }
}
