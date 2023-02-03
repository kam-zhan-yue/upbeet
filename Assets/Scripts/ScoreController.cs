using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public FloatConstant perfectThreshold;
    public IntReference perfectHits;
    public IntReference okayHits;
    public IntReference badHits;
    public IntReference combo;

    public void Init()
    {
        perfectHits.Value = 0;
        okayHits.Value = 0;
        badHits.Value = 0;
        combo.Value = 0;
    }

    public void ProcessTapNote(TapNote _note, float _tapTime)
    {
        if (!_note.CanHit)
            return;
        float tapNoteTime = _note.Position;
        float difference = Mathf.Abs(tapNoteTime - _tapTime);
        // Debug.Log("Difference is: "+ difference);
        if (difference <= perfectThreshold.Value)
        {
            perfectHits.Value++;
        }
        else
        {
            okayHits.Value++;
        }
        _note.RecordHit();
        combo.Value++;
    }

    public void RecordMiss(Note _note)
    {
        _note.RecordMiss();
        badHits.Value++;
        combo.Value = 0;
    }
    
    public void UnInit()
    {
        perfectHits.Value = 0;
        okayHits.Value = 0;
        badHits.Value = 0;
        combo.Value = 0;
    }
}
