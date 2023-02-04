using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UIRecord
{
    public string beatMap;
    public float score;
    public Rank rank;
    public int maxCombo;
    public int perfect;
    public int okay;
    public int bad;

    public UIRecord(string _beatMap)
    {
        beatMap = _beatMap;
        score = 0;
        rank = Rank.Unranked;
        maxCombo = 0;
        perfect = 0;
        okay = 0;
        bad = 0;
    }

    public UIRecord(ScoreSaveData _saveData)
    {
        beatMap = _saveData.beatMap;
        score = _saveData.score;
        rank = _saveData.rank;
        maxCombo = _saveData.maxCombo;
        perfect = _saveData.perfect;
        okay = _saveData.okay;
        bad = _saveData.bad;
    }
}
