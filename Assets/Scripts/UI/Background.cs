using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField]
    private RawImage _img;
    [SerializeField]
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        float speed = FlowSystem.Instance.GetFSMVariable<float>("Speed");
        //_img.uvRect = new Rect(_img.uvRect.position - (new Vector2(0, _speed) * Time.deltaTime), _img.uvRect.size);
        _img.uvRect = new Rect(_img.uvRect.position - (Vector2.up * Time.deltaTime * (speed) / 30), _img.uvRect.size);
    }
}
