using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    [FoldoutGroup("System Objects")] public SongController songController;
    
    [FoldoutGroup("UI Objects")] public MainMenuPopup mainMenuPopup;
    [FoldoutGroup("UI Objects")] public PausePopup pausePopup;
    [FoldoutGroup("UI Objects")] public GamePopup gamePopup;
    [FoldoutGroup("UI Objects")] public ResultsPopup resultPopup;
    
    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void PlaySong(BeatMap _beatMap)
    {
        songController.Init(_beatMap);
        songController.Play();
        gamePopup.ShowPopup();
    }

    public void PauseButtonPressed()
    {
        pausePopup.PauseButtonClicked();
    }

    public void PauseButtonClicked()
    {
        songController.Pause();
    }

    public void ResumeButtonClicked()
    {
        songController.Resume();
    }
    
    public void RestartButtonClicked()
    {
        songController.Restart();
    }

    public void ExitButtonClicked()
    {
        songController.Stop();
        songController.CloseNotePlayer();
        gamePopup.HidePopup();
        mainMenuPopup.ShowSongSelection();
    }

    public void ShowResults()
    {
        gamePopup.HidePopup();
        resultPopup.ShowPopup();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}