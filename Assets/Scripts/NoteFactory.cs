using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

public class NoteFactory : MonoBehaviour
{
    [Serializable]
    public class InitialisePool
    {
        public NoteType noteType;
        public Note notePrefab = null;
        public int initialPoolNum = 0;
        public int maxPoolNum = 0;
    }
    
    [TableList]
    public List<InitialisePool> initPoolList = new();
    
    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int defaultCapacity = 0;
    public int maxPoolSize = 0;
    
    public TapNote tapNotePrefab;
    public HoldNote holdNotePrefab;
    public FlickNote flickNotePrefab;

    private IObjectPool<TapNote> tapNotePool;
    public IObjectPool<TapNote> TapNotePool
    {
        get
        {
            if (tapNotePool == null)
                tapNotePool = new ObjectPool<TapNote>(CreateTapNote, OnTakeNote, OnReturnNote,
                    OnDestroyNote, collectionChecks, defaultCapacity, maxPoolSize);
            return tapNotePool;
        }
    }

    private IObjectPool<HoldNote> holdNotePool;
    public IObjectPool<HoldNote> HoldNotePool
    {
        get
        {
            if (holdNotePool == null)
                holdNotePool = new ObjectPool<HoldNote>(CreateHoldNote, OnTakeNote, OnReturnNote,
                    OnDestroyNote, collectionChecks, defaultCapacity, maxPoolSize);
            return holdNotePool;
        }
    }
    
    private IObjectPool<FlickNote> flickNotePool;
    public IObjectPool<FlickNote> FlickNotePool
    {
        get
        {
            if (flickNotePool == null)
                flickNotePool = new ObjectPool<FlickNote>(CreateFlickNote, OnTakeNote, OnReturnNote,
                    OnDestroyNote, collectionChecks, defaultCapacity, maxPoolSize);
            return flickNotePool;
        }
    }

    private TapNote CreateTapNote()
    {
        return Instantiate(tapNotePrefab);
    }

    private HoldNote CreateHoldNote()
    {
        return Instantiate(holdNotePrefab);
    }

    private FlickNote CreateFlickNote()
    {
        return Instantiate(flickNotePrefab);
    }

    // Called when an item is taken from the pool using Get
    private void OnTakeNote(Note _note)
    {
        _note.gameObject.SetActive(true);
    }
    
    // Called when an item is returned to the pool using Release
    private void OnReturnNote(Note _note)
    {
        _note.gameObject.SetActive(false);
    }
    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    private void OnDestroyNote(Note _note)
    {
        Destroy(_note.gameObject);
    }
}
