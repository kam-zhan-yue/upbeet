using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestingPopup : Popup
{
    [FoldoutGroup("System Objects")] public BeatMapDatabase beatMapDatabase;
    [FoldoutGroup("System Objects")] public SongController songController;
    
    [FoldoutGroup("UI Objects")] public TMP_Dropdown dropdown;
    [FoldoutGroup("UI Objects")] public TMP_InputField beatInputField;
    [FoldoutGroup("UI Objects")] public Button playButton;
    [FoldoutGroup("UI Objects")] public Button pauseButton;
    [FoldoutGroup("UI Objects")] public Button resumeButton;
    [FoldoutGroup("UI Objects")] public Button stopButton;
    [FoldoutGroup("UI Objects")] public TMP_Text beatText;

    public override void InitPopup()
    {
        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> optionList = new();
        for (int i = 0; i < beatMapDatabase.beatMapList.Count; ++i)
        {
            String songName = beatMapDatabase.beatMapList[i].name;
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = songName;
            optionList.Add(optionData);
        }
        dropdown.AddOptions(optionList);
        ShowPopup();
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
    }

    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
    }

    private void Update()
    {
        beatText.text = "Beat: " + songController.songPlayer.SongPositionInBeats;
    }

    public void Play()
    {
        String beatTextString = beatInputField.text;
        if (String.IsNullOrEmpty(beatTextString))
            PlaySong(-1);
        else if (int.TryParse(beatTextString, out int beat))
            PlaySong(beat);
        else
            Debug.LogError("Please input a beat first");
    }

    private void PlaySong(int _beat)
    {
        String beatMapName = dropdown.options[dropdown.value].text;
        if (!beatMapDatabase.TryGetBeatMap(beatMapName, out BeatMap beatMap))
            return;

        songController.Init(beatMap);
        if (_beat < 0)
            songController.Play();
        else
        {
            int beatMapBeat = beatMap.ConvertBeat(_beat);
            songController.Play(beatMapBeat);
        }
        dropdown.gameObject.SetActiveFast(false);
        playButton.gameObject.SetActiveFast(false);
        pauseButton.gameObject.SetActiveFast(true);
        stopButton.gameObject.SetActiveFast(true);
    }

    public void Pause()
    {
        songController.Pause();
        pauseButton.gameObject.SetActiveFast(false);
        resumeButton.gameObject.SetActiveFast(true);
    }
    
    public void Resume()
    {
        songController.Resume();
        pauseButton.gameObject.SetActiveFast(true);
        resumeButton.gameObject.SetActiveFast(false);
    }
    
    public void Stop()
    {
        songController.Stop();
        dropdown.gameObject.SetActiveFast(true);
        playButton.gameObject.SetActiveFast(true);
        stopButton.gameObject.SetActiveFast(false);
        pauseButton.gameObject.SetActiveFast(false);
        resumeButton.gameObject.SetActiveFast(false);
    }
}
