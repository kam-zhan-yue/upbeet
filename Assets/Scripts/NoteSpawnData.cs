using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnData
{
    public NoteType noteType;
    public int beat;
    public float position;
    public float trailEndPosition;
    public int trailEndBeatLength;
    public float trailDistance;

    public NoteSpawnData(NoteType _noteType, int _beat, float _position, int _trailEndBeatLength, float _trailEndPosition, float _trailDistance)
    {
        noteType = _noteType;
        beat = _beat;
        position = _position;
        trailEndBeatLength = _trailEndBeatLength;
        trailEndPosition = _trailEndPosition;
        trailDistance = _trailDistance;
    }
}
