using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beat Map Database", menuName = "Beat Map Database")]
public class BeatMapDatabase : SerializedScriptableObject
{
    public List<BeatMap> beatMapList = new();

    private Dictionary<BeatMap, ScoreSaveData> saveDictionary = new();

    public bool TryGetBeatMap(String _name, out BeatMap _beatMap)
    {
        for (int i = 0; i < beatMapList.Count; ++i)
        {
            if (beatMapList[i].name == _name)
            {
                _beatMap = beatMapList[i];
                return true;
            }
        }

        _beatMap = null;
        return false;
    }

    public void LoadSave(BeatMap _beatMap, ScoreSaveData _save)
    {
        saveDictionary.Add(_beatMap, _save);
    }

    public void InsertSave(ScoreSaveData _save)
    {
        if (!TryGetBeatMap(_save.beatMap, out BeatMap beatMap))
            return;
        
        //If there is already save data, check if need to update it
        if (saveDictionary.TryGetValue(beatMap, out ScoreSaveData saveData))
        {
            //If the old data has a higher score, don't try
            if (saveData.score > _save.score)
                return;
            //Else, update the new save dictionary
            saveDictionary[beatMap] = _save;
        }
        else
        {
            saveDictionary.Add(beatMap, _save);
        }
    }

    public bool TryGetSave(BeatMap _beatMap, out ScoreSaveData _saveData)
    {
        return saveDictionary.TryGetValue(_beatMap, out _saveData);
    }

    public bool CanSave(ScoreSaveData _save)
    {
        if (!TryGetBeatMap(_save.beatMap, out BeatMap beatMap))
            return false;
        
        //If there is already save data, check if need to update it
        if (saveDictionary.TryGetValue(beatMap, out ScoreSaveData saveData))
        {
            //If the old data has a higher score, don't try
            return !(saveData.score > _save.score);
        }
        return true;
    }

    public void ClearSave()
    {
        saveDictionary.Clear();
    }
}
