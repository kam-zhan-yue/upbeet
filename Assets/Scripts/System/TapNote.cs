using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Vector3 = UnityEngine.Vector3;

public class TapNote : Note
{
    public float scaleMultiplierX;
    public float scaleMultiplierY;
    public float scaleDuration;
    public Ease ease;
    
    public override void Move(float _deltaTime)
    {
        if (Hit)
        {
            //Stick it to the score threshold
            Transform transform1 = transform;
            Vector3 position = transform1.position;
            position.y = scoreThresholdY;
            position.y += HoldNote.OFFSET * scaleMultiplierY / 2;
            transform1.position = position;
        }
        else
        {
            Transform transform1 = transform;
            Vector3 position = transform1.position;
            position.y -= speed * _deltaTime;
            transform1.position = position;
        }
    }
    
    public override bool CanDespawn()
    {     
        return transform.position.y <= despawnThresholdY;
    }

    public override void RecordHit(float _tapTime)
    {
        if (!CanHit)
            return;
        base.RecordHit(_tapTime);
        float noteTime = Position;
        float difference = Mathf.Abs(noteTime - _tapTime);

        if (difference <= scoreController.perfectThreshold.Value)
        {
            scoreController.PerfectHit();
        }
        else if(difference <= scoreController.okayThreshold.Value)
        {
            scoreController.OkayHit();
        }
        
        Sequence sequence = DOTween.Sequence();
        Transform transform1 = transform;
        Vector3 originalLocalScale = transform1.localScale;
        Vector3 newLocalScale = originalLocalScale;
        newLocalScale.x *= scaleMultiplierX;
        newLocalScale.y *= scaleMultiplierY;
        sequence.Append(transform1.DOScale(newLocalScale, scaleDuration)).SetEase(ease);
        sequence.OnComplete(() =>
        {
            lane.PlayTapParticles();
            base.UnInit();
            transform1.localScale = originalLocalScale;
        });
    }

    public override void RecordMiss()
    {
        base.RecordMiss();
    }
}
