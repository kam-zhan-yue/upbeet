using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu(fileName = "New Score Controller", menuName = "Score Controller")]
public class ScoreController : SerializedScriptableObject
{
    public bool damageMode = false;
    public FloatConstant perfectThreshold;
    public FloatConstant okayThreshold;
    public IntReference perfectHits;
    public IntReference okayHits;
    public IntReference badHits;
    public IntReference combo;
    public FloatReference score;

    public void Init()
    {
        perfectHits.Value = 0;
        okayHits.Value = 0;
        badHits.Value = 0;
        combo.Value = 0;
    }

    public void RecordMiss(Note _note)
    {
        _note.RecordMiss();
        RecordMiss();
    }

    public void RecordMiss()
    {
        badHits.Value++;
        combo.Value = 0;
    }

    public void PerfectHit()
    {
        perfectHits.Value++;
        combo.Value++;
    }
    
    public void OkayHit()
    {
        okayHits.Value++;
        combo.Value++;
    }
    
    public void BadHit()
    {
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
