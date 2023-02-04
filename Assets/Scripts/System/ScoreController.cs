using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityAtoms.SceneMgmt;
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
    private Rank rank;
    private int maxCombo;

    public int MaxCombo => maxCombo;

    public void Init()
    {
        perfectHits.Value = 0;
        okayHits.Value = 0;
        badHits.Value = 0;
        combo.Value = 0;
        maxCombo = 0;
        rank = Rank.C;
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
        UpdateMaxCombo();
    }
    
    public void OkayHit()
    {
        okayHits.Value++;
        combo.Value++;
        UpdateMaxCombo();
    }

    private void UpdateMaxCombo()
    {
        if (combo.Value > maxCombo)
            maxCombo = combo.Value;
    }

    public ScoreSaveData ProcessResults(BeatMap _beatMap)
    {
        float totalPossibleScore = _beatMap.GetTotalPossibleScore();
        float percentage = score.Value / totalPossibleScore;
        rank = percentage switch
        {
            >= 0.85f => Rank.S,
            >= 0.70f => Rank.A,
            >= 0.55f => Rank.B,
            _ => Rank.C
        };
        return new(_beatMap.name, score, rank, maxCombo, perfectHits, okayHits, badHits);
    }

    public Rank GetRank()
    {
        return rank;
    }
    
    public void UnInit()
    {
        perfectHits.Value = 0;
        okayHits.Value = 0;
        badHits.Value = 0;
        combo.Value = 0;
        maxCombo = 0;
        rank = Rank.C;
    }
}
