using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreSaveData
{
    public string beatMap;
    public float score;
    public Rank rank;
    public int maxCombo;
    public int perfect;
    public int okay;
    public int bad;

    public void SetId(string _id)
    {
        beatMap = _id;
    }

    public ScoreSaveData(string _beatMap, float _score, Rank _rank, int _maxCombo, int _perfect, int _okay, int _bad)
    {
        beatMap = _beatMap;
        score = _score;
        rank = _rank;
        maxCombo = _maxCombo;
        perfect = _perfect;
        okay = _okay;
        bad = _bad;
    }
}
