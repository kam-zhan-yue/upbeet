using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SongSelectionPopup : Popup
{
    [FoldoutGroup("System Objects")] public BeatMapDatabase beatMapDatabase;
    
    [FoldoutGroup("UI Objects")] public RectTransform selectionHolder;
    [FoldoutGroup("UI Objects")] public SelectionPopupItem selectionPopupItemSample;

    private List<SelectionPopupItem> selectionList = new();

    protected override void InitPopup()
    {
        int numToSpawn = beatMapDatabase.beatMapList.Count - selectionList.Count;
        if (numToSpawn > 0)
        {
            selectionPopupItemSample.gameObject.SetActiveFast(true);
            for (int i = 0; i < numToSpawn; ++i)
            {
                SelectionPopupItem popupItem = Instantiate(selectionPopupItemSample, selectionHolder);
                selectionList.Add(popupItem);
            }
        }
        selectionPopupItemSample.gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        for (int i = 0; i < selectionList.Count; ++i)
        {
            if (i < beatMapDatabase.beatMapList.Count)
            {
                selectionList[i].gameObject.SetActiveFast(true);
                selectionList[i].Init(this, beatMapDatabase.beatMapList[i]);
            }
            else
            {
                selectionList[i].gameObject.SetActiveFast(false);
            }
        }
        base.ShowPopup();
    }

    public void SongClicked(BeatMap _beatMap)
    {
        Debug.Log("Play: "+_beatMap.name);
    }

    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
        base.HidePopup();
    }
}
