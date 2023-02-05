using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainMenuPopup : Popup
{
    [FoldoutGroup("UI Objects")] public TitlePopup titlePopup;
    [FoldoutGroup("UI Objects")] public SongSelectionPopup songSelectionPopup;
    [FoldoutGroup("UI Objects")] public CreditsPopup creditsPopup;
    [FoldoutGroup("UI Objects")] public TutorialPopup tutorialPopup;

    protected override void InitPopup()
    {
        ShowPopup();
    }

    public override void ShowPopup()
    {
        songSelectionPopup.HidePopup();
        titlePopup.ShowPopup();
    }

    public void PlayButtonClicked()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
        ShowSongSelection();
    }

    public void CreditsButtonClicked()
    {
        titlePopup.HidePopup();
        creditsPopup.ShowPopup();
    }

    public void TutorialButtonClicked()
    {
        titlePopup.HidePopup();
        tutorialPopup.ShowPopup();
    }

    public void BackButtonClicked()
    {
        if (songSelectionPopup.isShowing)
        {
            AudioManager.instance.Play(AudioManager.BUTTON);
            songSelectionPopup.HidePopup();
            titlePopup.ShowPopup();
        }

        else if (creditsPopup.isShowing)
        {
            creditsPopup.HidePopup();
            titlePopup.ShowPopup();
        }
        else if (tutorialPopup.isShowing)
        {
            tutorialPopup.HidePopup();
            titlePopup.ShowPopup();
        }
    }

    public void ShowSongSelection()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
        titlePopup.HidePopup();
        songSelectionPopup.ShowPopup();
    }

    public override void HidePopup()
    {
        throw new NotImplementedException();
    }
}
