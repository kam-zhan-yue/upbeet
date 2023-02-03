using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnData
{
    public NoteType noteType;
    public int beat;
    public float position;

    public NoteSpawnData(NoteType _noteType, int _beat, float _position)
    {
        noteType = _noteType;
        beat = _beat;
        position = _position;
    }
}
