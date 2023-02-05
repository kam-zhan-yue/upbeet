using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBox : MonoBehaviour
{
    public Lane lane;
    public ScoreController scoreController;
    public float localScaleSizeMultiplier = 0.8f;
    private Collider2D[] hitColliders = new Collider2D[10];

    public void Init(ScoreController _scoreController)
    {
        scoreController = _scoreController;
    }
    
    public void TapDown(float _time)
    {
        // Debug.Log("Tapping");
        if(lane != null && !lane.Dead)
            lane.OnPressDown();
        Transform transform1 = transform;
        int size = Physics2D.OverlapBoxNonAlloc(transform1.position, transform1.localScale * localScaleSizeMultiplier, 0f, hitColliders);
        for (int i = 0; i < size; ++i)
        {
            if (hitColliders[i] == null)
                continue;
            
            if(hitColliders[i].gameObject.TryGetComponent(out TapNote tapNote))
            {
                tapNote.RecordHit(_time);
            }
            else if (hitColliders[i].gameObject.TryGetComponent(out HoldNote holdNote))
            {
                holdNote.RecordHit(_time);
            }
        }
    }

    public void TapUp()
    {
        if(lane != null && !lane.Dead)
            lane.OnPressUp();
        Transform transform1 = transform;
        int size = Physics2D.OverlapBoxNonAlloc(transform1.position, transform1.localScale * localScaleSizeMultiplier, 0f, hitColliders);
        for (int i = 0; i < size; ++i)
        {
            if (hitColliders[i] == null)
                continue;
            if (hitColliders[i].gameObject.TryGetComponent(out HoldNote holdNote))
            {
                holdNote.RecordTapUp();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 position = gameObject.transform.position;
        Gizmos.DrawWireCube(position, transform.localScale * localScaleSizeMultiplier);
        // to visualize t$$anonymous$$s:
        // Physics.OverlapBox(pos, scale, rotation, ...)
    }
}
