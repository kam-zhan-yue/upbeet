using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TapNote : Note
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
}
