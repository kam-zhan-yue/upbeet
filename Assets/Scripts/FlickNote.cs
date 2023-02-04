using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlickNote : Note
{
    public override void Move(float _deltaTime)
    {
        Transform transform1 = transform;
        Vector3 position = transform1.position;
        position.y -= speed * _deltaTime;
        transform1.position = position;
    }
    
    public override bool CanDespawn()
    {     
        return transform.position.y <= despawnThresholdY;
    }

    public override void RecordHit(float _tapTime)
    {
        base.RecordHit(_tapTime);
    }

    public override void RecordMiss()
    {
        base.RecordMiss();
    }
}
