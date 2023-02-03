using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissThreshold : MonoBehaviour
{
    public ScoreController scoreController;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        Debug.Log("Collided");
        if (_collision.gameObject.TryGetComponent(out Note note))
        {
            Debug.Log("Miss!");
            scoreController.RecordMiss(note);
        }
    }
}
