using UnityEngine;

public abstract class Note : MonoBehaviour
{
    private Lane lane;
    private int beat;

    public void Init(Lane _lane, int _beat)
    {
        lane = _lane;
        beat = _beat;
    }

    public void UnInit()
    {
        lane.RemoveNote(this);
    }
}