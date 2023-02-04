using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBox : MonoBehaviour
{
    public ScoreController scoreController;
    private Collider2D[] hitColliders = new Collider2D[10];

    public void Init(ScoreController _scoreController)
    {
        scoreController = _scoreController;
    }
    
    public void TapDown(float _time)
    {
        Transform transform1 = transform;
        int size = Physics2D.OverlapBoxNonAlloc(transform1.position, transform1.localScale, 0f, hitColliders);
        for (int i = 0; i < size; ++i)
        {
            if (hitColliders[i] == null)
                continue;
            
            if(hitColliders[i].gameObject.TryGetComponent(out TapNote tapNote))
            {
                tapNote.RecordHit(_time);
                tapNote.UnInit();
            }
            else if (hitColliders[i].gameObject.TryGetComponent(out HoldNote holdNote))
            {
                holdNote.RecordHit(_time);
            }
        }
    }

    public void TapUp()
    {
        Transform transform1 = transform;
        int size = Physics2D.OverlapBoxNonAlloc(transform1.position, transform1.localScale, 0f, hitColliders);
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
        Gizmos.DrawWireCube(position, transform.localScale);
        // to visualize t$$anonymous$$s:
        // Physics.OverlapBox(pos, scale, rotation, ...)
    }
}
