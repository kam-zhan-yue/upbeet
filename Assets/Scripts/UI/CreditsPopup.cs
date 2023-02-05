using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPopup : Popup
{
    public RectTransform contentHolder;
    protected override void InitPopup()
    {
        
    }

    public override void ShowPopup()
    {
        contentHolder.gameObject.SetActiveFast(true);
        base.ShowPopup();
    }

    public override void HidePopup()
    {
        contentHolder.gameObject.SetActiveFast(false);
        base.HidePopup();
    }
}
