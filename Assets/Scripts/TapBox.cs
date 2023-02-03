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
        // int size = Physics.OverlapBoxNonAlloc(gameObject.transform.position, transform.localScale, hitColliders);
        Transform transform1 = transform;
        int size = Physics2D.OverlapBoxNonAlloc(transform1.position, transform1.localScale, 0f, hitColliders);
        for (int i = 0; i < size; ++i)
        {
            if(hitColliders[i] != null && hitColliders[i].gameObject.TryGetComponent<TapNote>(out TapNote tapNote))
            {
                scoreController.ProcessTapNote(tapNote, _time);
                tapNote.UnInit();
            }
        }
    }

    public void TapUp()
    {
        
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
