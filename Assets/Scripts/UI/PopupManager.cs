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
        mainMenuPopup.ShowSongSelection();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}