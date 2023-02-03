using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Random = UnityEngine.Random;

public class NotePlayer : MonoBehaviour
{
    public NoteFactory noteFactory;
    public List<Lane> laneList = new();
    public Transform scoreThreshold;
    public Transform despawnThreshold;

    private List<TapNote> tapNoteList = new();
    private List<HoldNote> holdNoteList = new();
    private List<FlickNote> flickNoteList = new();

    private void Awake()
    {
        Timing.RunCoroutine(TestSpawnNotes());
    }

    private IEnumerator<float> TestSpawnNotes()
    {
        while (true)
        {
            //Get all starting from 1 to avoid empty notes
            NoteType noteType = (NoteType) Random.Range(1, Enum.GetValues(typeof(NoteType)).Length);
            SpawnNote(noteType);
            Debug.Log("Spawning: "+noteType);
            yield return Timing.WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        for (int i = tapNoteList.Count-1; i >=0; --i)
        {
            Vector3 note = tapNoteList[i].transform.position;
            note.y -= 0.01f;
            tapNoteList[i].transform.position = note;
            if (CanDespawn(tapNoteList[i].transform))
            {
                noteFactory.TapNotePool.Release(tapNoteList[i]);
                tapNoteList.RemoveAt(i);
            }
        }
        
        for (int i = holdNoteList.Count-1; i >=0; --i)
        {
            Vector3 note = holdNoteList[i].transform.position;
            note.y -= 0.01f;
            holdNoteList[i].transform.position = note;
            if (CanDespawn(holdNoteList[i].transform))
            {
                noteFactory.HoldNotePool.Release(holdNoteList[i]);
                holdNoteList.RemoveAt(i);
            }
        }
        
        for (int i = flickNoteList.Count-1; i >=0; --i)
        {
            Vector3 note = flickNoteList[i].transform.position;
            note.y -= 0.01f;
            flickNoteList[i].transform.position = note;
            if (CanDespawn(flickNoteList[i].transform))
            {
                noteFactory.FlickNotePool.Release(flickNoteList[i]);
                flickNoteList.RemoveAt(i);
            }
        }
    }

    private bool CanDespawn(Transform _transform)
    {
        return _transform.position.y < despawnThreshold.position.y;
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines();
    }

    private void SpawnNote(NoteType _noteType)
    {
        Vector3 spawnPosition = laneList[0].spawnPoint.position;
        switch (_noteType)
        {
            case NoteType.Tap:
                TapNote tapNote = noteFactory.TapNotePool.Get();
                tapNote.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                tapNoteList.Add(tapNote);
                break;
            case NoteType.Hold:
                HoldNote holdNote = noteFactory.HoldNotePool.Get();
                holdNote.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                holdNoteList.Add(holdNote);
                break;
            case NoteType.Flick:
                FlickNote flickNote = noteFactory.FlickNotePool.Get();
                flickNote.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                flickNoteList.Add(flickNote);
                break;
            case NoteType.Empty:
            default:
                break;
        }
    }
}
