using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    public SpriteRenderer trailSpriteRenderer;
    public Transform headTransform;
    public Vector3 originalLocalScale;
    public float trailAlpha;
    public Transform trailNoteTransform;
    public const float OFFSET = 0.3f;
    public float scaleMultiplierX;
    public float scaleMultiplierY;
    public float scaleDuration;
    public Ease ease;
    private List<HoldStep> stepList = new();
    private bool heldDown = false;

    private bool playingParticles = false;
    // private bool sequencePlaying = false;

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

        heldDown = false;
        playingParticles = false;

        // headTransform.localScale = originalLocalScale;
        // headTransform.localPosition = Vector3.zero;
    }

    public override void SetColour(Color _colour)
    {
        base.SetColour(_colour);
        Color colour = _colour;
        colour.a = trailAlpha / 256;
        trailSpriteRenderer.color = colour;
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
                if (heldDown)
                {
                    if (!playingParticles)
                    {
                        AudioManager.instance.Play(AudioManager.HOLD);
                        lane.PlayHoldParticles();
                        playingParticles = true;
                    }
                }
                else
                {
                    if (playingParticles)
                    {
                        AudioManager.instance.Stop(AudioManager.HOLD);
                        lane.StopHoldParticles();
                        playingParticles = false;
                    }
                }
                //
                // if (heldDown)
                // {
                //     Vector3 headScale = headTransform.localScale;
                //     headScale.x = originalLocalScale.x * scaleMultiplierX;
                //     headScale.y = originalLocalScale.y * scaleMultiplierY;
                //     headTransform.localScale = headScale;
                //     
                //     //Stick it to the score threshold
                //     Vector3 position = headTransform.position;
                //     position.y = scoreThresholdY;
                //     position.y += OFFSET * scaleMultiplierY / 2;
                //     headTransform.position = position;
                // }
                // else
                // {
                //     headTransform.localScale = originalLocalScale;
                //     Vector3 position = headTransform.position;
                //     position.y = scoreThresholdY;
                //     position.y += OFFSET / 2;
                //     headTransform.position = position;
                // }
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

    private void Update()
    {
        CheckHold(notePlayer.songPosition);
    }

    public void CheckHold(float _songTime)
    {
        //CHeck initial miss
        float missTime = Position + scoreController.okayThreshold.Value;
        if (_songTime > missTime)
        {
            //If not missed and not hit, record the miss
            //Should only happen for the head note when it reaches the score threshold
            if (!Missed && !Hit)
            {
                RecordMiss();
                return;
            }
        }
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

        // if (heldDown)
        // {
        //     if (!sequencePlaying)
        //     {
        //         // Transform transform1 = headTransform;
        //         // Vector3 originalScale = transform1.localScale;
        //         // Vector3 newLocalScale = originalScale;
        //         // newLocalScale.x *= scaleMultiplierX;
        //         // newLocalScale.y *= scaleMultiplierY;
        //         //
        //         // Sequence sequence = DOTween.Sequence();
        //         // sequence.SetId(10);
        //         // sequence.Append(transform1.DOScale(newLocalScale, scaleDuration)).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        //         // lane.PlayHoldParticles();
        //         Transform transform1 = headTransform;
        //         Vector3 localScale = transform1.localScale;
        //         localScale.x *= scaleMultiplierX;
        //         localScale.y *= scaleMultiplierY;
        //         transform1.localScale = localScale;
        //         sequencePlaying = true;
        //     }
        // }
        // else
        // {
        //     if (sequencePlaying)
        //     {
        //         headTransform.localScale = originalLocalScale;
        //         DOTween.Kill(10);
        //         lane.StopHoldParticles();
        //         sequencePlaying = false;
        //     }
        // }
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
        return _transform.position.y <= scoreThresholdY + OFFSET / 2;
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
            // AudioManager.instance.Play(AudioManager.PERFECT);
            scoreController.PerfectHit();
        }
        else if(difference <= scoreController.okayThreshold.Value)
        {
            AudioManager.instance.Play(AudioManager.OKAY);
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

    public override void UnInit()
    {
        lane.StopHoldParticles();
        base.UnInit();
    }
    
}
