using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SelectionPopupItem : MonoBehaviour
{
    [FoldoutGroup("UI Objects")] public TMP_Text title;

    private SongSelectionPopup songSelectionPopup;
    private BeatMap beatMap;

    public void Init(SongSelectionPopup _popup, BeatMap _beatMap)
    {
        songSelectionPopup = _popup;
        beatMap = _beatMap;
        title.text = beatMap.name;
    }

    public void SongClicked()
    {
        songSelectionPopup.SongClicked(beatMap);
    }
}
