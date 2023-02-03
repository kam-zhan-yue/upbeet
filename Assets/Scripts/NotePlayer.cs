using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class NotePlayer : MonoBehaviour
{
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
            laneList[i].Init(this);
        }
    }

    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Play(int _beat)
    {
        StartingSecond = _beat * beatMap.SecPerBeat;
        AllocateLanes();
        for (int i = 0; i < laneList.Count; ++i)
        {
            laneList[i].StartedPlaying = true;
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
                if (noteType == NoteType.Empty)
                    continue;
                
                float beatPositionInSeconds = beatMap.GetBeatPositionInSeconds(j);
                NoteSpawnData spawnData = new(noteType, j, beatPositionInSeconds);
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
        CheckCanDespawn();
    }

    private void CheckCanDespawn()
    {
        for (int i = tapNoteList.Count-1; i >=0; --i)
        {
            if (CanDespawn(tapNoteList[i].transform))
            {
                tapNoteList[i].UnInit();
            }
        }
        
        for (int i = holdNoteList.Count-1; i >=0; --i)
        {
            if (CanDespawn(holdNoteList[i].transform))
            {
                holdNoteList[i].UnInit();
            }
        }
        
        for (int i = flickNoteList.Count-1; i >=0; --i)
        {
            if (CanDespawn(flickNoteList[i].transform))
            {
                flickNoteList[i].UnInit();
            }
        }
    }

    private bool CanDespawn(Transform _transform)
    {
        return _transform.position.y <= despawnThreshold.position.y;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
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
    }
}
