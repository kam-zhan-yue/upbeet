using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beat Map", menuName = "Beat Map")]
public class BeatMap : SerializedScriptableObject
{
    //Public Setup Variables
    [BoxGroup("Setup")]
    public AudioClip song;
    
    [BoxGroup("Setup")]
    public int songBpm;
    
    [BoxGroup("Setup")]
    public int hitsPerBeat = 1;
    
    [BoxGroup("Setup")]
    public int tracks = 1;
    
    //Logic Variables
    public float SecPerBeat => 60f / (songBpm * hitsPerBeat);
    
    //Beat Map Variables
    [PropertyOrder(20)]
    [TitleGroup("Beat Map")]
    [TableMatrix(DrawElementMethod = "DrawCell", RowHeight = 20)]
    public NoteType[,] beatMap;

    [PropertyOrder(10)]
    [FoldoutGroup("Editor Functions")]
    [Button]
    public void InitBeatMap()
    {
        if (song == null)
        {
            Debug.LogWarning("Put a song in the beat map first!");
            return;
        }
        float songDuration = song.length;
        int totalBeats = (int)(songDuration / SecPerBeat);
        beatMap = new NoteType[tracks, totalBeats];
    }

    public NoteType GetNoteType(int _track, int _beat)
    {
        int lowerBound0 = beatMap.GetLowerBound(0);
        int upperBound0 = beatMap.GetUpperBound(0);
        int lowerBound1 = beatMap.GetLowerBound(1);
        int upperBound1 = beatMap.GetUpperBound(1);
        if (_track < lowerBound0 ||
            _track > upperBound0 ||
            _beat < lowerBound1 ||
            _beat > upperBound1)
            return NoteType.Empty;
        return beatMap[_track, _beat];
    }

    private static NoteType DrawCell(Rect _rect, NoteType _value)
    {
        if (Event.current.type == EventType.MouseDown && _rect.Contains(Event.current.mousePosition))
        {
            _value = _value switch
            {
                //Change the value of the cell
                NoteType.Empty => NoteType.Tap,
                NoteType.Tap => NoteType.Hold,
                NoteType.Hold => NoteType.Flick,
                NoteType.Flick => NoteType.Empty,
                _ => _value
            };
            GUI.changed = true;
            Event.current.Use();
        }
        
        //Pick the Colour according to the note type
        Color colour = _value switch
        {
            NoteType.Empty => StaticHelper.EmptyColour,
            NoteType.Tap => StaticHelper.TapColour,
            NoteType.Hold => StaticHelper.HoldColour,
            NoteType.Flick => StaticHelper.FlickColour,
            _ => StaticHelper.EmptyColour
        };
        EditorGUI.DrawRect(_rect.Padding(1), colour);
        return _value;
    }

    public int ConvertBeat(int _currentBeat)
    {
        float songDuration = song.length;
        int totalBeats = (int)(songDuration / SecPerBeat);
        //Minus 1 because of the array
        int reverseBeat = totalBeats - _currentBeat - 1;
        return reverseBeat;
    }
}
