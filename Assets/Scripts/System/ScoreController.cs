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

    [FoldoutGroup("Scoring Parameters")] public float basePoints;
    [FoldoutGroup("Scoring Parameters")] public float incrementAmount;
    
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
        score.Value = 0;
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
        AudioManager.instance.Play(AudioManager.BAD);
    }

    public void PerfectHit()
    {
        perfectHits.Value++;
        combo.Value++;
        UpdateMaxCombo();
        float noteScore = GetNoteScore(Grade.Perfect, combo.Value, NoteType.Tap);
        score.Value += noteScore;
    }
    
    public void OkayHit()
    {
        okayHits.Value++;
        combo.Value++;
        UpdateMaxCombo();
        float noteScore = GetNoteScore(Grade.Okay, combo.Value, NoteType.Tap);
        score.Value += noteScore;
        AudioManager.instance.Play(AudioManager.OKAY);
    }

    private void UpdateMaxCombo()
    {
        if (combo.Value > maxCombo)
            maxCombo = combo.Value;
    }

    public ScoreSaveData ProcessResults(BeatMap _beatMap, bool _cleared)
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
        bool fullCombo = maxCombo == _beatMap.GetTotalPossibleCombo();
        return new(_beatMap.name, score, rank, maxCombo, perfectHits, okayHits, badHits, fullCombo, _cleared);
    }

    public Rank GetRank()
    {
        return rank;
    }

    [Button]
    public float GetNoteScore(Grade _grade, int _combo, NoteType _noteType)
    {
        if (_noteType == NoteType.Empty || _noteType == NoteType.Flick)
            return 0f;
        if (_grade == Grade.Bad)
            return 0f;

        int increments;
        if (_combo <= 10)
            increments = 0;
        else if (_combo <= 20)
            increments = 1;
        else if (_combo <= 30)
            increments = 2;
        else if (_combo <= 40)
            increments = 3;
        else if (_combo <= 50)
            increments = 4;
        else if (_combo <= 60)
            increments = 5;
        else if (_combo <= 70)
            increments = 6;
        else if (_combo <= 80)
            increments = 7;
        else if (_combo <= 90)
            increments = 8;
        else if (_combo <= 100)
            increments = 9;
        else
            increments = 10;
        
        float points = basePoints;
        float multipliedPoints = points + increments * incrementAmount;
        
        float finalPoints = multipliedPoints;
        if (_grade == Grade.Perfect)
            finalPoints *= 2;
        return finalPoints;
    }
    
    public void UnInit()
    {
        perfectHits.Value = 0;
        okayHits.Value = 0;
        badHits.Value = 0;
        combo.Value = 0;
        maxCombo = 0;
        score.Value = 0;
        rank = Rank.C;
    }
}
