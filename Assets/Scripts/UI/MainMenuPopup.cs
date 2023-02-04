using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainMenuPopup : Popup
{
    [FoldoutGroup("UI Objects")] public TitlePopup titlePopup;
    [FoldoutGroup("UI Objects")] public SongSelectionPopup songSelectionPopup;

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
        ShowSongSelection();
    }

    public void BackButtonClicked()
    {
        if (songSelectionPopup.isShowing)
        {
            songSelectionPopup.HidePopup();
            titlePopup.ShowPopup();
        }
    }

    public void ShowSongSelection()
    {
        titlePopup.HidePopup();
        songSelectionPopup.ShowPopup();
    }

    public override void HidePopup()
    {
        throw new NotImplementedException();
    }
}
