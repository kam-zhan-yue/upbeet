using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausePopup : Popup
{
    [FoldoutGroup("System Objects")] public BoolVariable gamePlaying;
    
    [FoldoutGroup("UI Objects")] public RectTransform mainHolder;
    [FoldoutGroup("UI Objects")] public RectTransform mediaHolder;
    [FoldoutGroup("UI Objects")] public CountdownPopupItem countdownItem;
    

    private PlayerControls playerControls;
    private bool countdownActive = false;
    
    protected override void InitPopup()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.started += PauseStarted;
        playerControls.Enable();
        mainHolder.gameObject.SetActiveFast(false);
        HidePopup();
    }

    private void PauseStarted(InputAction.CallbackContext _callbackContext)
    {
        PauseButtonClicked();
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        mainHolder.gameObject.SetActiveFast(true);
        mediaHolder.gameObject.SetActiveFast(true);
        countdownItem.gameObject.SetActiveFast(false);
        base.ShowPopup();
    }
    
    public void PauseButtonClicked()
    {
        if (!gamePlaying.Value)
            return;
        if (countdownActive)
            return;
        
        if (isShowing)
        {
            ResumeButtonClicked();
        }
        else
        {
            PopupManager.instance.PauseButtonClicked();
            ShowPopup();
        }
    }

    public void SettingsButtonClicked()
    {
        
    }

    public void ResumeButtonClicked()
    {
        mediaHolder.gameObject.SetActiveFast(false);
        countdownActive = true;
        countdownItem.Activate(() =>
        {
            countdownActive = false;
            PopupManager.instance.ResumeButtonClicked();
            mainHolder.gameObject.SetActiveFast(false);
            HidePopup();
        });
    }

    public void RestartButtonClicked()
    {
        PopupManager.instance.RestartButtonClicked();
        mainHolder.gameObject.SetActiveFast(false);
        HidePopup();
    }

    public void ExitButtonClicked()
    {
        PopupManager.instance.ExitButtonClicked();
        mainHolder.gameObject.SetActiveFast(false);
        HidePopup();
    }

    public override void HidePopup()
    {
        base.HidePopup();
    }

    private void OnDestroy()
    {
        playerControls.Player.Pause.started -= PauseStarted;
        playerControls.Disable();
    }
}
