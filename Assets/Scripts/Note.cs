using UnityEngine;
using UnityEngine.Pool;

public abstract class Note : MonoBehaviour
{
    private NotePlayer notePlayer;
    private Lane lane;
    private int beat;

    public void Init(NotePlayer _notePlayer, Lane _lane, int _beat)
    {
        notePlayer = _notePlayer;
        lane = _lane;
        beat = _beat;
    }

    public virtual void UnInit()
    {
        if(lane != null)
            lane.RemoveNote(this);
        notePlayer.RemoveNote(this);
    }
}