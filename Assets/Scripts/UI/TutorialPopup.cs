using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : Popup
{
    public RectTransform contentHolder;
    protected override void InitPopup()
    {
        
    }

    public override void ShowPopup()
    {
        PopupManager.instance.ShowTutorial();
        contentHolder.gameObject.SetActiveFast(true);
        base.ShowPopup();
    }

    public override void HidePopup()
    {
        PopupManager.instance.HideTutorial();
        contentHolder.gameObject.SetActiveFast(false);
        base.HidePopup();
    }
}
