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
    public bool Missed => missed;
    public bool Hit => hit;
    public bool CanHit => !missed && !hit;

    public float Position => position;

    public void Init(NotePlayer _notePlayer, Lane _lane, int _beat, float _position)
    {
        notePlayer = _notePlayer;
        lane = _lane;
        beat = _beat;
        position = _position;
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