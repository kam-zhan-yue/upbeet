using UnityEngine;
using UnityEngine.Pool;

public abstract class Note : MonoBehaviour
{
    public ScoreController scoreController;
    private NotePlayer notePlayer;
    private Lane lane;
    private int beat;
    private float position;
    private bool missed = false;
    private bool hit = false;
    protected float speed = 0f;
    protected float scoreThresholdY = 0f;
    protected float despawnThresholdY = 0f;
    public bool Missed => missed;
    public bool Hit => hit;
    public int Beat => beat;
    public bool CanHit => !missed && !hit;

    public float Position => position;

    public virtual void Init(NotePlayer _notePlayer, Lane _lane, NoteSpawnData _spawnData)
    {
        notePlayer = _notePlayer;
        lane = _lane;
        beat = _spawnData.beat;
        position = _spawnData.position;
        speed = _spawnData.speed;
        scoreThresholdY = _spawnData.scoreThresholdY;
        despawnThresholdY = _spawnData.despawnThresholdY;
        missed = false;
        hit = false;
    }
    
    public virtual void UnInit()
    {
        if(lane != null)
            lane.RemoveNote(this);
        notePlayer.RemoveNote(this);
    }

    public virtual void RecordHit(float _tapTime)
    {
        hit = true;
    }
    
    public virtual void RecordMiss()
    {
        missed = true;
        if(scoreController.damageMode && lane != null)
            lane.RemoveLife();
    }

    public abstract void Move(float _deltaTime);

    public abstract bool CanDespawn();
}