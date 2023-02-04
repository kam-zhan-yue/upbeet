using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public bool godMode = false;
    public bool IsPlaying { get; set; }
    public float SecondsIntoTrack { get; set; }
    public bool Dead => lives <= 0;

    public Transform laneBackground;
    private List<NoteSpawnData> noteSpawnList = new();
    private List<Note> noteList = new();
    private NotePlayer notePlayer;
    private const float OFFSCREEN_DISTANCE = 10.0f;
    private int nextNoteToSpawn_idx = 0;
    private int lives = 0;

    public void Init(NotePlayer _notePlayer, int _lives)
    {
        notePlayer = _notePlayer;
        lives = _lives;
        ClearNotes();
    }

    private void ClearNotes()
    {
        nextNoteToSpawn_idx = 0;
        noteSpawnList.Clear();
    }

    public void AddNoteSpawnData(NoteSpawnData _spawnData)
    {
        noteSpawnList.Add(_spawnData);
    }

    private void Update()
    {
        if (IsPlaying)
        {
            SecondsIntoTrack += Time.deltaTime;

            // check which notes to spawn
            while (nextNoteToSpawn_idx < noteSpawnList.Count)
            {
                float distance = notePlayer.noteSpeed * (noteSpawnList[nextNoteToSpawn_idx].position - SecondsIntoTrack);
                if (distance > OFFSCREEN_DISTANCE)
                {
                    break;
                }

                Vector3 spawnPosition = notePlayer.scoreThreshold.position;

                // set x to the lane itself and adjust y to the right distance
                spawnPosition.x = transform.position.x;
                spawnPosition.y += distance;

                // spawn the note using the note player
                Note note = notePlayer.InstantiateNote(noteSpawnList[nextNoteToSpawn_idx].noteType, spawnPosition);
                NoteSpawnData spawnData = noteSpawnList[nextNoteToSpawn_idx];
                if (spawnData.CanSpawn())
                {
                    note.Init(notePlayer, this, spawnData);
                    noteList.Add(note);
                }

                // now check the next note
                nextNoteToSpawn_idx++;
            }

            // move the notes
            for (int i = noteList.Count - 1; i >= 0; --i)
            {
                MoveNote(noteList[i]);
            }
        }
    }
    
    private void MoveNote(Note _note)
    {
        _note.Move(Time.deltaTime);
    }

    public void RemoveNote(Note _note)
    {
        noteList.Remove(_note);
    }

    public void RemoveLife()
    {
        if (godMode)
            return;
        Debug.Log("Remove Life");
        lives--;
        if (lives <= 0)
        {
            for (int i = noteList.Count - 1; i >= 0; --i)
            {
                //Remove all active notes from the spawn list before giving it
                //to other lanes
                RemoveSpawnData(noteList[i].Beat);
                noteList[i].UnInit();
            }
            notePlayer.ReportLaneDead(this);
            ClearNotes();
        }
    }

    private void RemoveSpawnData(int _beat)
    {
        for (int i = noteSpawnList.Count -1 ; i >= 0; --i)
        {
            if (noteSpawnList[i].beat == _beat)
            {
                noteSpawnList.RemoveAt(i);
                break;
            }
        }
    }

    public List<NoteSpawnData> GetSpawnList()
    {
        return noteSpawnList;
    }

    public bool TryAddSpawnData(NoteSpawnData _spawnData)
    {
        NoteType noteType = _spawnData.noteType;
        //Don't add hold trails, unless processing a regular hold note
        if (noteType == NoteType.HoldTrail)
            return false;
        
        //For tap notes, the trail length is 1, so need to offset by a positive 1
        for (int i = 0; i < _spawnData.trailSpawnData.trailBeatLength + 1; ++i)
        {
            int beatToCheck = _spawnData.beat - i;
            //If there is another note at this beat, then cannot add
            if (IsBeatBusy(beatToCheck))
                return false;
        }
        
        Debug.Log($"{gameObject.name} Adding {noteType} at {_spawnData.beat} with trail {_spawnData.trailSpawnData.trailBeatLength}");
        noteSpawnList.Add(_spawnData);
        for (int i = 0; i < _spawnData.trailSpawnData.trailBeatLength; ++i)
        {
            int beatToInsert = _spawnData.beat - (i+1);
            noteSpawnList.Add(NoteSpawnData.GetHoldTrailData(beatToInsert));
            Debug.Log($"{gameObject.name} Adding trail at {beatToInsert}");
        }
        
        return true;
    }

    private bool IsBeatBusy(int _beat)
    {
        for (int i = 0; i < noteSpawnList.Count; ++i)
        {
            if (noteSpawnList[i].beat == _beat)
                return true;
        }
        return false;
    }

    public void SortSpawnData()
    {
        SpawnComparer spawnComparer = new();
        noteSpawnList.Sort(spawnComparer);
    }
}

public class SpawnComparer : IComparer<NoteSpawnData>
{
    public int Compare(NoteSpawnData _x, NoteSpawnData _y)
    {
        if (_y != null && _x != null)
        {
            int beatComparison = _y.beat.CompareTo(_x.beat);
            if (beatComparison != 0) return beatComparison;
        }
        return 0;
    }
}