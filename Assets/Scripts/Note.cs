using UnityEngine;
using UnityEngine.Pool;

public abstract class Note : MonoBehaviour
{
    private NotePlayer notePlayer;
    private Lane lane;
    private int beat;
    private float position;
    private bool missed = false;
    private bool hit = false;
    private int trailEndBeat = 0;
    private float trailEndBeatPosition = 0f;
    public bool Missed => missed;
    public bool Hit => hit;
    public bool CanHit => !missed && !hit;

    public float Position => position;

    public virtual void Init(NotePlayer _notePlayer, Lane _lane, NoteSpawnData _spawnData)
    {
        notePlayer = _notePlayer;
        lane = _lane;
        beat = _spawnData.beat;
        position = _spawnData.position;
        trailEndBeat = _spawnData.trailEndBeatLength;
        trailEndBeatPosition = _spawnData.trailEndPosition;
        missed = false;
        hit = false;
    }

    public void RecordMiss()
    {
        missed = true;
    }

    public virtual void UnInit()
    {
        if(lane != null)
            lane.RemoveNote(this);
        notePlayer.RemoveNote(this);
    }

    public void RecordHit()
    {
        hit = true;
    }
}