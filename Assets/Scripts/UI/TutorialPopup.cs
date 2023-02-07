using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TutorialPopup : Popup
{
    [FoldoutGroup("UI Objects")] public RectTransform contentHolder;
    [FoldoutGroup("UI Objects")] public RectTransform mobileButtonHolder;

    protected override void InitPopup()
    {
        
    }

    public override void ShowPopup()
    {
        PopupManager.instance.ShowTutorial();
        contentHolder.gameObject.SetActiveFast(true);
        mobileButtonHolder.gameObject.SetActiveFast(PopupManager.instance.isMobile());
        base.ShowPopup();
    }

    public override void HidePopup()
    {
        PopupManager.instance.HideTutorial();
        contentHolder.gameObject.SetActiveFast(false);
        base.HidePopup();
    }
}
