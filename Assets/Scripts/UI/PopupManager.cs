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
        AudioManager.instance.Play(AudioManager.ROOTS);
    }

    public void PlaySong(BeatMap _beatMap)
    {
        AudioManager.instance.Stop(AudioManager.ROOTS);
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
        gamePopup.ShowPopup();
    }

    public void ExitButtonClicked()
    {
        AudioManager.instance.Play(AudioManager.ROOTS);
        songController.Stop();
        songController.CloseNotePlayer();
        gamePopup.HidePopup();
        mainMenuPopup.ShowSongSelection();
    }

    public void ShowResults(bool _cleared, bool _maxCombo)
    {
        gamePopup.HidePopup();
        resultPopup.SetClearedAndFullCombo(_cleared, _maxCombo);
        resultPopup.ShowPopup();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
    }
}