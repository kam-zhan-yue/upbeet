using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    [FoldoutGroup("System Objects")] public SongController songController;
    [FoldoutGroup("System Objects")] public TutorialObject tutorialObject;
    
    [FoldoutGroup("UI Objects")] public MainMenuPopup mainMenuPopup;
    [FoldoutGroup("UI Objects")] public PausePopup pausePopup;
    [FoldoutGroup("UI Objects")] public GamePopup gamePopup;
    [FoldoutGroup("UI Objects")] public ResultsPopup resultPopup;
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
         return IsMobile();
#endif
#if UNITY_EDITOR
        return true;
#endif
        return false;
    }
    
    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
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

    public void ShowTutorial()
    {
        tutorialObject.Show();
    }

    public void HideTutorial()
    {
        tutorialObject.Hide();
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

    public void OnMobileTapDown(int _lane)
    {
        songController.GetCurrentPlayerController().OnMobileTapDown(_lane);
    }

    public void OnMobileTapUp(int _lane)
    {
        songController.GetCurrentPlayerController().OnMobileTapUp(_lane);
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
    }
}