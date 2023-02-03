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
        Debug.Log($"Trail Distance {_spawnData.trailDistance}, Trail End Position: {_spawnData.trailEndPosition}");
    }
}
