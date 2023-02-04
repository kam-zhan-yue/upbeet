using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GamePopup : Popup
{
    [FoldoutGroup("System Objects")] public ScoreController scoreController;
    
    [FoldoutGroup("UI Objects")] public RectTransform contentHolder;
    [FoldoutGroup("UI Objects")] public TMP_Text scoreText;
    [FoldoutGroup("UI Objects")] public TMP_Text perfectText;
    [FoldoutGroup("UI Objects")] public TMP_Text okayText;
    [FoldoutGroup("UI Objects")] public TMP_Text badText;
    [FoldoutGroup("UI Objects")] public RectTransform comboHolder;
    [FoldoutGroup("UI Objects")] public TMP_Text comboText;
    
    protected override void InitPopup()
    {
        contentHolder.gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
        base.ShowPopup();
        gameObject.SetActiveFast(true);
        contentHolder.gameObject.SetActiveFast(true);
        UpdateDetails();
    }

    public void PauseButtonClicked()
    {
        PopupManager.instance.PauseButtonPressed();
    }

    private void UpdateDetails()
    {
        UpdateCombo();
        UpdateScore();
        perfectText.gameObject.SetActiveFast(false);
        okayText.gameObject.SetActiveFast(false);
        badText.gameObject.SetActiveFast(false);
    }

    public void UpdateCombo()
    {
        int combo = scoreController.combo;
        if (combo > 0)
        {
            comboHolder.gameObject.SetActiveFast(true);
            comboText.text = combo.ToString();
        }
        else
        {
            comboHolder.gameObject.SetActiveFast(false);
        }
    }

    public void UpdateScore()
    {
        scoreText.text = scoreController.score.Value.ToString(CultureInfo.InvariantCulture);
    }

    public void OnPerfectHit(Int32 _perfectHits)
    {
        if (_perfectHits <= 0)
            return;
        perfectText.gameObject.SetActiveFast(true);
        okayText.gameObject.SetActiveFast(false);
        badText.gameObject.SetActiveFast(false);
    }

    public void OnOkayHit(Int32 _okayHits)
    {
        if (_okayHits <= 0)
            return;
        perfectText.gameObject.SetActiveFast(false);
        okayText.gameObject.SetActiveFast(true);
        badText.gameObject.SetActiveFast(false);
    }

    public void OnBadHit(Int32 _badHits)
    {
        if (_badHits <= 0)
            return;
        perfectText.gameObject.SetActiveFast(false);
        okayText.gameObject.SetActiveFast(false);
        badText.gameObject.SetActiveFast(true);
    }

    public override void HidePopup()
    {
        base.HidePopup();
        contentHolder.gameObject.SetActiveFast(false);
        gameObject.SetActiveFast(false);
    }
}
