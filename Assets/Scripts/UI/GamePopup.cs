using System;
using System.Globalization;
using DG.Tweening;
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
    
    [FoldoutGroup("Parameters")] public float scaleDuration;
    [FoldoutGroup("Parameters")] public Ease scaleEase;
    [FoldoutGroup("Parameters")] public float stayDuration;

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
            DOTween.Kill(0);
            comboHolder.gameObject.SetActiveFast(true);
            comboText.text = combo.ToString();
            GameObject comboObject = comboText.gameObject;
            comboObject.SetActiveFast(true);
            comboObject.transform.localScale = Vector3.zero;
            Sequence sequence = DOTween.Sequence();
            sequence.SetId(0);
            sequence.Append(comboObject.transform.DOScale(Vector3.one, scaleDuration).SetEase(scaleEase));
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
        
        ClearSequences();
        GameObject textObject = perfectText.gameObject;
        textObject.SetActiveFast(true);
        textObject.transform.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.SetId(1);
        sequence.Append(textObject.transform.DOScale(Vector3.one, scaleDuration).SetEase(scaleEase));
        sequence.AppendInterval(stayDuration);
        sequence.OnComplete(() =>
        {
            textObject.gameObject.SetActiveFast(false);
        });
        okayText.gameObject.SetActiveFast(false);
        badText.gameObject.SetActiveFast(false);
    }

    public void OnOkayHit(Int32 _okayHits)
    {
        if (_okayHits <= 0)
            return;
        perfectText.gameObject.SetActiveFast(false);
        
        ClearSequences();
        GameObject textObject = okayText.gameObject;
        textObject.SetActiveFast(true);
        textObject.transform.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.SetId(2);
        sequence.Append(textObject.transform.DOScale(Vector3.one, scaleDuration).SetEase(scaleEase));
        sequence.AppendInterval(stayDuration);
        sequence.OnComplete(() =>
        {
            textObject.gameObject.SetActiveFast(false);
        });
        badText.gameObject.SetActiveFast(false);
    }

    public void OnBadHit(Int32 _badHits)
    {
        if (_badHits <= 0)
            return;
        perfectText.gameObject.SetActiveFast(false);
        okayText.gameObject.SetActiveFast(false);
        
        ClearSequences();
        GameObject textObject = badText.gameObject;
        textObject.SetActiveFast(true);
        textObject.transform.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.SetId(3);
        sequence.Append(textObject.transform.DOScale(Vector3.one, scaleDuration).SetEase(scaleEase));
        sequence.AppendInterval(stayDuration);
        sequence.OnComplete(() =>
        {
            textObject.gameObject.SetActiveFast(false);
        });
    }

    private void ClearSequences()
    {
        DOTween.Kill(1);
        DOTween.Kill(2);
        DOTween.Kill(3);
    }

    public override void HidePopup()
    {
        base.HidePopup();
        contentHolder.gameObject.SetActiveFast(false);
        gameObject.SetActiveFast(false);
    }
}
