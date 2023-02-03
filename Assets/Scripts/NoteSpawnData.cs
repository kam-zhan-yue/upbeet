using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnData
{
    public NoteType noteType;
    public int beat;
    public float position;
    public float trailDistance;
    public float speed = 0f;
    public float scoreThresholdY = 0f;
    public float despawnThresholdY = 0f;

    public NoteSpawnData(NoteType _noteType, int _beat, float _position, float _trailDistance, float _speed, float _scoreThreshold, float _despawnThreshold)
    {
        noteType = _noteType;
        beat = _beat;
        position = _position;
        trailDistance = _trailDistance;
        speed = _speed;
        scoreThresholdY = _scoreThreshold;
        despawnThresholdY = _despawnThreshold;
    }
}
