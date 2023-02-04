using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ResultsPopup : Popup
{
    [FoldoutGroup("System Objects")] public ScoreController scoreController;
    
    [FoldoutGroup("UI Objects")] public RectTransform contentHolder;
    [FoldoutGroup("UI Objects")] public RectTransform sRankHolder;
    [FoldoutGroup("UI Objects")] public RectTransform aRankHolder;
    [FoldoutGroup("UI Objects")] public RectTransform bRankHolder;
    [FoldoutGroup("UI Objects")] public RectTransform cRankHolder;
    [FoldoutGroup("UI Objects")] public TMP_Text scoreText;
    [FoldoutGroup("UI Objects")] public TMP_Text maxComboText;
    [FoldoutGroup("UI Objects")] public TMP_Text perfectText;
    [FoldoutGroup("UI Objects")] public TMP_Text okayText;
    [FoldoutGroup("UI Objects")] public TMP_Text badText;

    protected override void InitPopup()
    {
        contentHolder.gameObject.SetActiveFast(false);
        HidePopup();
    }

    public override void ShowPopup()
    {
        base.ShowPopup();
        contentHolder.gameObject.SetActiveFast(true);
        UpdateRank();
        UpdateStats();
    }

    private void UpdateRank()
    {
        Rank rank = scoreController.GetRank();
        sRankHolder.gameObject.SetActiveFast(rank == Rank.S);
        aRankHolder.gameObject.SetActiveFast(rank == Rank.A);
        bRankHolder.gameObject.SetActiveFast(rank == Rank.B);
        cRankHolder.gameObject.SetActiveFast(rank == Rank.C);
    }

    private void UpdateStats()
    {
        scoreText.text = Mathf.RoundToInt(scoreController.score.Value).ToString();
        maxComboText.text = scoreController.MaxCombo.ToString();
        perfectText.text = scoreController.perfectHits.Value.ToString();
        okayText.text = scoreController.okayHits.Value.ToString();
        badText.text = scoreController.badHits.Value.ToString();
    }

    public void RestartButtonClicked()
    {
        PopupManager.instance.RestartButtonClicked();
        contentHolder.gameObject.SetActiveFast(false);
        HidePopup();
    }

    public void ExitButtonClicked()
    {
        PopupManager.instance.ExitButtonClicked();
        contentHolder.gameObject.SetActiveFast(false);
        HidePopup();
    }
    
    public override void HidePopup()
    {
        base.HidePopup();
    }
}
