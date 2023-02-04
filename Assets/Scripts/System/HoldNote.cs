using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HoldNote : Note
{
    private class HoldStep
    {
        private readonly float time = 0f;
        private bool flag = false;

        public HoldStep(float _time)
        {
            time = _time;
            flag = false;
        }

        public bool CanCheck(float _songTime)
        {
            if (flag)
                return false;
            if (_songTime >= time)
            {
                flag = true;
                return true;
            }
            return false;
        }

        public bool Checked()
        {
            return flag;
        }
    }
    
    public const float OFFSET = 0.2f;
    public Transform trailNoteTransform;
    private List<HoldStep> stepList = new();
    private bool heldDown = false;

    public override void Init(NotePlayer _notePlayer, Lane _lane, NoteSpawnData _spawnData)
    {
        base.Init(_notePlayer, _lane, _spawnData);
        //Set the position to be halfway between the distance and the wave note
        Vector3 localPosition = trailNoteTransform.localPosition;
        localPosition.y = _spawnData.trailSpawnData.trailDistance * 0.5f;
        trailNoteTransform.localPosition = localPosition;
        
        //Set the scale to be the length of the distance
        Vector3 localScale = trailNoteTransform.localScale;
        //Minus offset so that it will cover up until the end of the next note
        localScale.y = _spawnData.trailSpawnData.trailDistance - OFFSET;
        trailNoteTransform.localScale = localScale;

        //Add all the hold steps
        stepList.Clear();
        float startTime = Position;
        float endTime = _spawnData.trailSpawnData.trailEndPosition;
        endTime -= _spawnData.trailSpawnData.offsetTime;
        int beatLength = _spawnData.trailSpawnData.trailBeatLength;
        float stepGap = (endTime - startTime) / (beatLength * 2);
        //Minus one so that the last step is removed because it is too hard otherwise
        //So if there are 3 beat lengths in the trail, the note potential is 5
        for (int i = 0; i < beatLength * 2 - 1; ++i)
        {
            float stepTime = stepGap * (i + 1);
            stepTime += startTime;
            HoldStep step = new(stepTime);
            stepList.Add(step);
        }
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

    public void CheckInitialMiss(float _songTime)
    {
        float missTime = Position + scoreController.okayThreshold.Value;
        if(_songTime > missTime)
            RecordMiss();
    }

    public void CheckHold(float _songTime)
    {
        for (int i = 0; i < stepList.Count; ++i)
        {
            if (!stepList[i].CanCheck(_songTime))
                continue;

            if (heldDown)
                RecordHold();
            else
            {
                //Don't bother with the hit and miss checking. This is for sure miss area
                base.RecordMiss();
                scoreController.RecordMiss();
            }
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
        return ReachedScoreThreshold(transform) && TrailNoteReduced() && AllStepsChecked();
    }

    private bool AllStepsChecked()
    {
        for (int i = 0; i < stepList.Count; ++i)
        {
            if (!stepList[i].Checked())
                return false;
        }

        return true;
    }

    private bool ReachedScoreThreshold(Transform _transform)
    {
        return _transform.position.y <= scoreThresholdY + OFFSET;
    }

    private bool TrailNoteReduced()
    {
        return trailNoteTransform.localScale.y <= 0;
    }

    public override void RecordHit(float _tapTime)
    {
        heldDown = true;
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
    }

    private void RecordHold()
    {
        scoreController.PerfectHit();
    }

    public void RecordTapUp()
    {
        heldDown = false;
    }

    public override void RecordMiss()
    {
        if (!Missed && !Hit)
        {
            base.RecordMiss();
            scoreController.RecordMiss();
        }
    }
}
