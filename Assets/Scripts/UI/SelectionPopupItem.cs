using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionPopupItem : MonoBehaviour
{
    [FoldoutGroup("UI Objects")] public TMP_Text title;
    [FoldoutGroup("UI Objects")] public SongSelectionPopup songSelectionPopup;

    private BeatMap beatMap;

    public void Init(BeatMap _beatMap)
    {
        beatMap = _beatMap;
        title.text = beatMap.name;
    }
    
    public void OnPointerEnter(BaseEventData _eventData)
    {
        songSelectionPopup.ShowDetail(beatMap);
    }

    public void OnPointerExit(BaseEventData _eventData)
    {
        songSelectionPopup.HideDetail();
    }
    
    public void SongClicked()
    {
        PopupManager.instance.PlaySong(beatMap);
    }
}
