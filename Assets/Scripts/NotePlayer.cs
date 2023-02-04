using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

public class NotePlayer : MonoBehaviour
{
    [BoxGroup("Setup")] public FloatReference songPosition;
    [BoxGroup("Setup")] public IntReference laneLives;
    [BoxGroup("Setup")] public NoteFactory noteFactory;
    [BoxGroup("Setup")] public List<Lane> laneList = new();
    [BoxGroup("Setup")] public Transform scoreThreshold;
    [BoxGroup("Setup")] public Transform despawnThreshold;
    [BoxGroup("Variables")] public float noteSpeed = 0;
        
    public BeatMap beatMap;

    public float StartingSecond { get; set; } = 0;

    private List<TapNote> tapNoteList = new();
    private List<HoldNote> holdNoteList = new();
    private List<FlickNote> flickNoteList = new();

    public void Init(BeatMap _beatMap)
    {
        beatMap = _beatMap;
        for (int i = 0; i < laneList.Count; ++i)
        {
            laneList[i].Init(this, laneLives);
        }
    }

    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Play(int _beat)
    {
        StartingSecond = _beat * beatMap.SecPerBeat;
        AllocateLanes();
        foreach (Lane lane in laneList)
        {
            lane.IsPlaying = true;
            lane.SecondsIntoTrack = StartingSecond;
        }
    }

    private void AllocateLanes()
    {
        int lanes = beatMap.GetTotalLanes();
        int beats = beatMap.GetTotalBeats();
        
        //TODO: Implement a way to dynamically change the number of lanes!
        if (laneList.Count < lanes)
        {
            Debug.LogWarning("There are not enough lanes for this song!");
            return;
        }

        for (int i = 0; i < lanes; ++i)
        {
            for (int j = beats - 1; j > 0; --j)
            {
                //Skip if the note is empty
                NoteType noteType = beatMap.GetNoteType(i, j);
                if (noteType is NoteType.Empty or NoteType.HoldTrail)
                    continue;
                
                float beatPositionInSeconds = beatMap.GetBeatPositionInSeconds(j);
                
                //Get the length and the end beat in array index form
                int trailEndBeatLength = beatMap.GetEndTrailBeatLength(i, j);
                
                //Minus 1 to help it reach the end of the next note
                int trailEndBeat = j - trailEndBeatLength - 1;
                
                //Get the position of the end of the trail and deduct for trail time
                float trailEndPosition = beatMap.GetBeatPositionInSeconds(trailEndBeat);
                float trailDistance = (trailEndPosition - beatPositionInSeconds) * noteSpeed;

                NoteSpawnData.TrailSpawnData trailSpawnData = new()
                {
                    trailBeatLength = trailEndBeatLength,
                    trailEndPosition = trailEndPosition,
                    trailDistance = trailDistance,
                    offsetTime = HoldNote.OFFSET / noteSpeed
                };

                NoteSpawnData spawnData = new(noteType, j, beatPositionInSeconds, trailSpawnData, 
                    noteSpeed, scoreThreshold.position.y, despawnThreshold.position.y);
                laneList[i].AddNoteSpawnData(spawnData);
            }
        }
    }
    
    public Note InstantiateNote(NoteType _noteType, Vector3 _position)
    {
        switch (_noteType)
        {
            case NoteType.Tap:
                TapNote tapNote = noteFactory.TapNotePool.Get();
                tapNote.transform.SetPositionAndRotation(_position, Quaternion.identity);
                tapNoteList.Add(tapNote);
                return tapNote;
            case NoteType.Hold:
                HoldNote holdNote = noteFactory.HoldNotePool.Get();
                holdNote.transform.SetPositionAndRotation(_position, Quaternion.identity);
                holdNoteList.Add(holdNote);
                return holdNote;
            case NoteType.Flick:
                FlickNote flickNote = noteFactory.FlickNotePool.Get();
                flickNote.transform.SetPositionAndRotation(_position, Quaternion.identity);
                flickNoteList.Add(flickNote);
                return flickNote;
            case NoteType.Empty:
            default:
                break;
        }

        return null;
    }

    private void Update()
    {
        CheckHoldNote();
        CheckCanDespawn();
    }

    private void CheckHoldNote()
    {
        for (int i = holdNoteList.Count - 1; i >= 0; --i)
        {
            holdNoteList[i].CheckInitialMiss(songPosition);
            holdNoteList[i].CheckHold(songPosition);
        }
    }

    private void CheckCanDespawn()
    {
        for (int i = tapNoteList.Count-1; i >=0; --i)
        {
            if (tapNoteList[i].CanDespawn())
            {
                tapNoteList[i].UnInit();
            }
        }
        
        for (int i = holdNoteList.Count-1; i >=0; --i)
        {
            if (holdNoteList[i].CanDespawn())
            {
                holdNoteList[i].UnInit();
            }
        }
        
        for (int i = flickNoteList.Count-1; i >=0; --i)
        {
            if (flickNoteList[i].CanDespawn())
            {
                flickNoteList[i].UnInit();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        foreach (Lane lane in laneList)
        {
            lane.IsPlaying = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        foreach (Lane lane in laneList)
        {
            lane.IsPlaying = true;
        }
    }

    public void RemoveNote(Note _note)
    {
        switch (_note)
        {
            case TapNote note:
                tapNoteList.Remove(note);
                noteFactory.TapNotePool.Release(note);
                break;
            case HoldNote note:
                holdNoteList.Remove(note);
                noteFactory.HoldNotePool.Release(note);
                break;
            case FlickNote note:
                flickNoteList.Remove(note);
                noteFactory.FlickNotePool.Release(note);
                break;
        }
    }

    public void Stop()
    {
        for (int i = tapNoteList.Count-1; i >=0; --i)
        {
            tapNoteList[i].UnInit();
        }
        
        for (int i = holdNoteList.Count-1; i >=0; --i)
        {
            holdNoteList[i].UnInit();
        }
        
        for (int i = flickNoteList.Count-1; i >=0; --i)
        {
            flickNoteList[i].UnInit();
        }
        foreach (Lane lane in laneList)
        {
            lane.IsPlaying = false;
        }
    }
}
