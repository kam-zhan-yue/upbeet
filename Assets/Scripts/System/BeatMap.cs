using System;
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
    public int lanes = 1;
    
    //Logic Variables
    public float SecPerBeat => 60f / (songBpm * hitsPerBeat);

    [NonSerialized, ShowInInspector, ReadOnly]
    private float totalPossibleScore = 0f;

    [NonSerialized, ShowInInspector, ReadOnly]
    private int totalPossibleBeats = 0;
    
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
        beatMap = new NoteType[lanes, totalBeats];
    }

    public NoteType GetNoteType(int _lane, int _beat)
    {
        int lowerBound0 = beatMap.GetLowerBound(0);
        int upperBound0 = beatMap.GetUpperBound(0);
        int lowerBound1 = beatMap.GetLowerBound(1);
        int upperBound1 = beatMap.GetUpperBound(1);
        if (_lane < lowerBound0 ||
            _lane > upperBound0 ||
            _beat < lowerBound1 ||
            _beat > upperBound1)
            return NoteType.Empty;
        return beatMap[_lane, _beat];
    }

    public int GetEndTrailBeatLength(int _lane, int _beat)
    {
        NoteType noteType = GetNoteType(_lane, _beat);
        if (noteType != NoteType.Hold)
            return 0;
        int trails = 0;
        //Start from the wave beat and move up
        for (int i = _beat-1; i >= 0; --i)
        {
            NoteType type = GetNoteType(_lane, i);
            if (type == NoteType.HoldTrail)
                trails++;
            else
                break;
        }
        return trails;
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
                NoteType.Hold => NoteType.HoldTrail,
                NoteType.HoldTrail =>  NoteType.Empty,
                // NoteType.Flick => NoteType.Empty,
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
            NoteType.HoldTrail => StaticHelper.HoldTrailColour,
            NoteType.Flick => StaticHelper.FlickColour,
            _ => StaticHelper.EmptyColour
        };
        EditorGUI.DrawRect(_rect.Padding(1), colour);
        return _value;
    }

    public float GetBeatPositionInSeconds(int _beat)
    {
        int realBeat = ConvertBeat(_beat);
        return realBeat * SecPerBeat;
    }

    public int ConvertBeat(int _currentBeat)
    {
        float songDuration = song.length;
        int totalBeats = (int)(songDuration / SecPerBeat);
        //Minus 1 because of the array
        int reverseBeat = totalBeats - _currentBeat - 1;
        return reverseBeat;
    }

    public int GetTotalLanes()
    {
        return beatMap.GetUpperBound(0) + 1;
    }

    public int GetTotalBeats()
    {
        return beatMap.GetUpperBound(1) + 1;
    }

    public float GetTotalPossibleScore()
    {
        return totalPossibleScore;
    }

    private void OnValidate()
    {
        CalculatePointsAndBeats();
    }

    private void CalculatePointsAndBeats()
    {
        totalPossibleBeats = 0;
        totalPossibleScore = 0f;
        int laneNum = GetTotalLanes();
        int beatNum = GetTotalBeats();
        for (int i = 0; i < laneNum; ++i)
        {
            for (int j = 0; j < beatNum; ++j)
            {
                NoteType noteType = GetNoteType(i, j);
                if (noteType is NoteType.Empty or NoteType.HoldTrail or NoteType.Flick)
                    continue;
                int trailBeatEndLength = GetEndTrailBeatLength(i, j);
                if (trailBeatEndLength > 0)
                {
                    int trailBeatNum = trailBeatEndLength * 2 - 1;
                    totalPossibleBeats += trailBeatNum;
                }
                totalPossibleBeats++;
            }
        }

        float basePoints = 300;
        float incrementAmount = 100;
        int incrementStep = 10;
        int maxIncrements = 10;
        int increments = 0;
        
        for (int i = 0; i < totalPossibleBeats; ++i)
        {
            if (i > 0 && i % incrementStep == 0 && increments < maxIncrements)
                increments++;
            totalPossibleScore += basePoints + incrementAmount * increments;
        }

        //Factor in the perfect modifier
        totalPossibleScore *= 2f;
    }
}
