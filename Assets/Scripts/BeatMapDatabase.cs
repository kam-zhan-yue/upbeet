using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beat Map Database", menuName = "Beat Map Database")]
public class BeatMapDatabase : SerializedScriptableObject
{
    public List<BeatMap> beatMapList = new();

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
}
