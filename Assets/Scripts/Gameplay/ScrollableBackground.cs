using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableBackground : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer renderer;
    private Vector2 savedOffset;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(0, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
