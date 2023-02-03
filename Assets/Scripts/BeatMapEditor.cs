using System;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;

public class BeatMapEditor : OdinEditorWindow
{
    [MenuItem("Beat Map/Editor")]
    private static void OpenWindow()
    {
        GetWindow<BeatMapEditor>().Show();
    }

    [TitleGroup("Beat Map")]
    [PropertyOrder(-20)]
    public BeatMap beatMap;

    [TitleGroup("Beat Map")]
    [PropertyOrder(-20)]
    public int beatPlayback = 0;

    [TitleGroup("Heat Map")]
    [OnInspectorGUI("Tick")] 
    [NonSerialized, ShowInInspector, ReadOnly]
    private int currentBeat;

    private GameObject songPlayerObject;
    private SongPlayer songPlayer;
    private bool active = false;

    [PropertyOrder(-10)]
    [HorizontalGroup("Media Buttons")]
    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
    public void Play()
    {
        if (active)
            return;
        if (songPlayer == null || songPlayerObject == null)
        {
            songPlayerObject = new("Test Song PLayer");
            songPlayerObject.hideFlags = HideFlags.HideAndDontSave;
            songPlayer = songPlayerObject.AddComponent<SongPlayer>();
        }
        songPlayer.Init(beatMap);
        int beatMapBeat = beatMap.ConvertBeat(beatPlayback);
        Debug.Log("Playing Beat: "+beatMapBeat);
        songPlayer.Play(beatMapBeat);
        active = true;
    }

    [HorizontalGroup("Media Buttons")]
    [Button(ButtonSizes.Large), GUIColor(1, 0f, 0)]
    public void Stop()
    {
        if (songPlayer == null || songPlayerObject == null)
            return;
        songPlayer.Stop();
        active = false;
    }
    
    private void Tick()
    {
        if (beatMap == null || songPlayer == null)
            return;
        if (!active)
        {
            currentBeat = 0;
            return;
        }
        songPlayer.Tick();
        currentBeat = songPlayer.CurrentBeat;
        Repaint();
    }
    
    [HorizontalGroup("Lanes")]
    [GUIColor("GetLaneOneColour")]
    [Button("", ButtonSizes.Large)]
    public void LaneOne()
    {
    }
    
    [HorizontalGroup("Lanes")]
    [GUIColor("GetLaneTwoColour")]
    [Button("", ButtonSizes.Large)]
    public void LaneTwo()
    {
    }
    
    [HorizontalGroup("Lanes")]
    [GUIColor("GetLaneThreeColour")]
    [Button("", ButtonSizes.Large)]
    public void LaneThree()
    {
    }
    
    [HorizontalGroup("Lanes")]
    [GUIColor("GetLaneFourColour")]
    [Button("", ButtonSizes.Large)]
    public void LaneFour()
    {
    }
    
    private Color GetLaneOneColour()
    {
        return GetTileColour(0);
    }
    
    private Color GetLaneTwoColour()
    {
        return GetTileColour(1);
    }
    
    private Color GetLaneThreeColour()
    {
        return GetTileColour(2);
    }
    
    private Color GetLaneFourColour()
    {
        return GetTileColour(3);
    }
    
    private Color GetTileColour(int _lane)
    {
        if (beatMap == null || !active)
        {
            return Color.black;
        }

        int beatMapBeat = beatMap.ConvertBeat(currentBeat);
        NoteType note = beatMap.GetNoteType(_lane, beatMapBeat);
        return note switch
        {
            NoteType.Empty => StaticHelper.EmptyColour,
            NoteType.Tap => StaticHelper.TapColour,
            NoteType.Hold => StaticHelper.HoldColour,
            NoteType.Flick => StaticHelper.FlickColour,
            _ => StaticHelper.EmptyColour
        };
    }
}
