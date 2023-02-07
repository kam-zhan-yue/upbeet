using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private const string SAVE_PATH = "/Saves/";
    private const string TXT = ".txt";
    public BeatMapDatabase beatMapDatabase;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        beatMapDatabase.ClearSave();

        EnsureDirectoryExists();

        Load();
    }

    private void EnsureDirectoryExists()
    {
        if (!Directory.Exists(Application.dataPath + SAVE_PATH))
            Directory.CreateDirectory(Application.dataPath + SAVE_PATH);
    }

    public void Save(ScoreSaveData _save)
    {
        if (beatMapDatabase.CanSave(_save))
        {
            beatMapDatabase.InsertSave(_save);
            string jsonString = JsonUtility.ToJson(_save);
            string file = Application.dataPath + SAVE_PATH + _save.beatMap + TXT;
            File.WriteAllText(file, jsonString);
            Debug.Log($"Saving to {file}\n{jsonString}");
        }
    }

    private void Load()
    {
        //Find existing save files on the device. If there are no save files, then cannot do anything
        for (int i = 0; i < beatMapDatabase.beatMapList.Count; ++i)
        {
            string beatMapName = beatMapDatabase.beatMapList[i].name;
            string file = Application.dataPath + SAVE_PATH + beatMapName + TXT;
            if (!File.Exists(file))
            {
                Debug.Log($"No file found at {file}");
                continue;
            }

            Debug.Log($"Getting data from {file}");
            string saveString = File.ReadAllText(file);
            ScoreSaveData saveData = JsonUtility.FromJson<ScoreSaveData>(saveString);
            beatMapDatabase.LoadSave(beatMapDatabase.beatMapList[i], saveData);
        }
    }

    private void UnInit()
    {
        //Clear temporary save data in scriptable object
        beatMapDatabase.ClearSave();
    }

    private void OnDestroy()
    {
        UnInit();
    }
}
