using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainMenuPopup : Popup
{
    [FoldoutGroup("UI Objects")] public TitlePopup titlePopup;
    [FoldoutGroup("UI Objects")] public SongSelectionPopup songSelectionPopup;

    public override void InitPopup()
    {
        ShowPopup();
    }

    public override void ShowPopup()
    {
        songSelectionPopup.gameObject.SetActiveFast(false);
        
        titlePopup.ShowPopup();
    }

    public override void HidePopup()
    {
        throw new NotImplementedException();
    }
}
