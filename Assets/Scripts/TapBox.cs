using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBox : MonoBehaviour
{
    Collider2D[] hitColliders = new Collider2D[10];
    
    public void TapDown()
    {
        // int size = Physics.OverlapBoxNonAlloc(gameObject.transform.position, transform.localScale, hitColliders);
        Transform transform1 = transform;
        int size = Physics2D.OverlapBoxNonAlloc(transform1.position, transform1.localScale, 0f, hitColliders);
        for (int i = 0; i < size; ++i)
        {
            if(hitColliders[i] != null && hitColliders[i].gameObject.TryGetComponent<TapNote>(out TapNote tapNote))
            {
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
