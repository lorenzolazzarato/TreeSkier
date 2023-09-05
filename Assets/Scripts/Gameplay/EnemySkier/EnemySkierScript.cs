using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkierScript : HittableObject
{

    
    [SerializeField]
    private float _MinXSpeed = -1f;

    [SerializeField] 
    private float _MaxXSpeed = 1f;



    [SerializeField]
    private float _MovementSpeed = 3f;

    
    private float xSpeed = 0;

    private SpriteRenderer _sprite;
    

    public override void Setup()
    {
        base.Setup();

        xSpeed = Random.Range(_MinXSpeed, _MaxXSpeed);

        _sprite = GetComponentInChildren<SpriteRenderer>();

        _sprite.flipX = xSpeed > 0;
    }

    public override void Clear()
    {
        base.Clear();

        xSpeed = 0;
    }

    public override void HitObject()
    {
        base.HitObject();
        AudioManager.Instance.PlaySoundEffect(_ObjectIdContainer);
    }

    protected override void Update()
    {
        base.Update();
        transform.Translate(Vector3.right * xSpeed * Time.deltaTime * _MovementSpeed);
    }
}
