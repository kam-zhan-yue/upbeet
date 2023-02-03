using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HoldNote : Note
{
    private const float OFFSET = 0.1f;
    public Transform trailNoteTransform;

    public override void Init(NotePlayer _notePlayer, Lane _lane, NoteSpawnData _spawnData)
    {
        base.Init(_notePlayer, _lane, _spawnData);
        //Set the position to be halfway between the distance and the wave note
        Vector3 localPosition = trailNoteTransform.localPosition;
        localPosition.y = _spawnData.trailDistance * 0.5f;
        trailNoteTransform.localPosition = localPosition;
        
        //Set the scale to be the length of the distance
        Vector3 localScale = trailNoteTransform.localScale;
        //Minus offset twice so that it will cover up until the end of the next note
        localScale.y = _spawnData.trailDistance - OFFSET * 2;
        trailNoteTransform.localScale = localScale;
    }

    public override void Move(float _deltaTime)
    {
        Transform transform1 = transform;
        //If at the score threshold, stop if there is a trail. Continue if no trail
        if (ReachedScoreThreshold(transform1))
        {
            if (!TrailNoteReduced())
            {
                //Drop the center point lower and lower
                Vector3 localPosition = trailNoteTransform.localPosition;
                localPosition.y -= speed * _deltaTime * 0.5f;
                trailNoteTransform.localPosition = localPosition;
                //Reduce the local scale lower and lower
                Vector3 localScale = trailNoteTransform.localScale;
                localScale.y -= speed * _deltaTime;
                trailNoteTransform.localScale = localScale;
            }
            else
            {
                MoveDown(transform1, _deltaTime);
            }
        }
        else
        {
            MoveDown(transform1, _deltaTime);
        }
    }

    private void MoveDown(Transform _transform, float _deltaTime)
    {
        Vector3 position = _transform.position;
        position.y -= speed * _deltaTime;
        _transform.position = position;
    }
    
    public override bool CanDespawn()
    {
        return ReachedScoreThreshold(transform) && TrailNoteReduced();
    }

    private bool ReachedScoreThreshold(Transform _transform)
    {
        return _transform.position.y <= scoreThresholdY;
    }

    private bool TrailNoteReduced()
    {
        return trailNoteTransform.localScale.y <= 0;
    }
}
