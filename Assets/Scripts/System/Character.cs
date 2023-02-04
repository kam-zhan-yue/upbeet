using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public AnimationClip idle;
    public AnimationClip tap;
    public AnimationClip hold;
    public AnimationClip lose;

    private void Awake()
    {
        animator.enabled = true;
    }

    [Button]
    public void PlayIdle()
    {
        animator.Play(idle.name);
    }

    [Button]
    public void PlayTap()
    {
        animator.Play(tap.name);
    }

    [Button]
    public void PlayHold()
    {
        animator.Play(hold.name);
    }

    [Button]
    public void PlayLose()
    {
        animator.Play(lose.name);
    }
}
