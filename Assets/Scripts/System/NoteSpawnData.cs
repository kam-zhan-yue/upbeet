using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoteSpawnData
{
    public class TrailSpawnData
    {
        public int trailBeatLength;
        public float trailEndPosition;
        public float trailDistance;
        public float offsetTime;
    }
    public NoteType noteType;
    public int beat;
    public float position;
    public TrailSpawnData trailSpawnData;
    public float speed = 0f;
    public float scoreThresholdY = 0f;
    public float despawnThresholdY = 0f;

    public NoteSpawnData(NoteType _noteType, int _beat, float _position, TrailSpawnData _trailSpawnData, float _speed, float _scoreThreshold, float _despawnThreshold)
    {
        noteType = _noteType;
        beat = _beat;
        position = _position;
        trailSpawnData = _trailSpawnData;
        speed = _speed;
        scoreThresholdY = _scoreThreshold;
        despawnThresholdY = _despawnThreshold;
    }

    private NoteSpawnData() { }

    public static NoteSpawnData GetHoldTrailData(int _beat)
    {
        NoteSpawnData trailSpawn = new()
        {
            noteType = NoteType.HoldTrail,
            beat = _beat
        };
        return trailSpawn;
    }

    public bool CanSpawn()
    {
        return noteType != NoteType.Empty && noteType != NoteType.HoldTrail;
    }
}
