using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SelectionPopupItem : MonoBehaviour
{
    [FoldoutGroup("UI Objects")] public TMP_Text title;

    private BeatMap beatMap;

    public void Init(BeatMap _beatMap)
    {
        beatMap = _beatMap;
        title.text = beatMap.name;
    }

    public void SongClicked()
    {
        PopupManager.instance.PlaySong(beatMap);
    }
}
