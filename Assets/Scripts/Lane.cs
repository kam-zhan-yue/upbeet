using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public bool IsPlaying { get; set; }
    public float SecondsIntoTrack { get; set; }

    public Transform laneBackground;
    private List<NoteSpawnData> noteSpawnList = new();
    private List<Note> noteList = new();
    private NotePlayer notePlayer;
    private const float OFFSCREEN_DISTANCE = 10.0f;
    private int nextNoteToSpawn_idx = 0;

    public void Init(NotePlayer _notePlayer)
    {
        notePlayer = _notePlayer;
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
                note.Init(notePlayer, this, spawnData);
                noteList.Add(note);

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
}
