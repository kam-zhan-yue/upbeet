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
    [FoldoutGroup("UI Objects")] public RectTransform notClearedHolder;
    [FoldoutGroup("UI Objects")] public TMP_Text scoreText;
    [FoldoutGroup("UI Objects")] public TMP_Text maxComboText;
    [FoldoutGroup("UI Objects")] public TMP_Text perfectText;
    [FoldoutGroup("UI Objects")] public TMP_Text okayText;
    [FoldoutGroup("UI Objects")] public TMP_Text badText;
    [FoldoutGroup("UI Objects")] public TMP_Text fullComboText;

    private bool cleared = false;
    private bool fullCombo = false;
    
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
        if (cleared)
        {
            AudioManager.instance.Play(AudioManager.APPLAUSE);
        }
    }

    public void SetClearedAndFullCombo(bool _cleared, bool _fullCombo)
    {
        cleared = _cleared;
        fullCombo = _fullCombo;
    }

    private void UpdateRank()
    {
        Rank rank = scoreController.GetRank();
        sRankHolder.gameObject.SetActiveFast(rank == Rank.S && cleared);
        aRankHolder.gameObject.SetActiveFast(rank == Rank.A && cleared);
        bRankHolder.gameObject.SetActiveFast(rank == Rank.B && cleared);
        cRankHolder.gameObject.SetActiveFast(rank == Rank.C && cleared);
        notClearedHolder.gameObject.SetActiveFast(!cleared);
    }

    private void UpdateStats()
    {
        scoreText.text = Mathf.RoundToInt(scoreController.score.Value).ToString();
        maxComboText.text = scoreController.MaxCombo.ToString();
        perfectText.text = scoreController.perfectHits.Value.ToString();
        okayText.text = scoreController.okayHits.Value.ToString();
        badText.text = scoreController.badHits.Value.ToString();
        fullComboText.gameObject.SetActiveFast(fullCombo);
    }

    public void RestartButtonClicked()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
        PopupManager.instance.RestartButtonClicked();
        contentHolder.gameObject.SetActiveFast(false);
        HidePopup();
        AudioManager.instance.Stop(AudioManager.APPLAUSE);
    }

    public void ExitButtonClicked()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
        PopupManager.instance.ExitButtonClicked();
        contentHolder.gameObject.SetActiveFast(false);
        HidePopup();
        AudioManager.instance.Stop(AudioManager.APPLAUSE);
    }
    
    public override void HidePopup()
    {
        base.HidePopup();
    }
}
