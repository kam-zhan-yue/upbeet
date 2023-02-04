using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SelectionDetailPopupItem : MonoBehaviour
{
    [FoldoutGroup("UI Objects")] public TMP_Text beatMapText;
    [FoldoutGroup("UI Objects")] public RectTransform recordContent;
    [FoldoutGroup("UI Objects")] public RectTransform noSaveDataContent;
    [FoldoutGroup("UI Objects")] public TMP_Text rankText;
    [FoldoutGroup("UI Objects")] public TMP_Text scoreText;
    [FoldoutGroup("UI Objects")] public TMP_Text maxComboText;
    [FoldoutGroup("UI Objects")] public TMP_Text perfectText;
    [FoldoutGroup("UI Objects")] public TMP_Text okayText;
    [FoldoutGroup("UI Objects")] public TMP_Text badText;
    
    private UIRecord record;

    public void Init(UIRecord _record)
    {
        record = _record;
    }

    public void Show()
    {
        gameObject.SetActiveFast(true);
        if (record.rank == Rank.Unranked)
        {
            recordContent.gameObject.SetActiveFast(false);
            noSaveDataContent.gameObject.SetActiveFast(true);
            beatMapText.text = record.beatMap;
        }
        else
        {
            recordContent.gameObject.SetActiveFast(true);
            noSaveDataContent.gameObject.SetActiveFast(false);
            beatMapText.text = record.beatMap;
            rankText.text = record.rank.ToString();
            scoreText.text = Mathf.RoundToInt(record.score).ToString();
            maxComboText.text = record.maxCombo.ToString();
            perfectText.text = record.perfect.ToString();
            okayText.text = record.okay.ToString();
            badText.text = record.bad.ToString();
        }
    }

    public void Hide()
    {
        gameObject.SetActiveFast(false);
    }
}
