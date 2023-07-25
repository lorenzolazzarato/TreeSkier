using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenTreeScript : TreeScript
{
    private float rotation;

    private SpriteRenderer tree;

    public override void Setup()
    {
        base.Setup();

        rotation = Mathf.Sign(Random.Range(-1, 1)) * 90;

        tree = GetComponentInChildren<SpriteRenderer>();

        tree.transform.Rotate(0, 0, rotation);
    }

    public override void Clear()
    {
        base.Clear();

        tree.transform.Rotate(0, 0, -rotation);
    }
}
