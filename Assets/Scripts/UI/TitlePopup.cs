using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class TitlePopup : Popup
{
    [FoldoutGroup("UI Objects")] public RectTransform[] buttonTransforms = Array.Empty<RectTransform>();
    [FoldoutGroup("Parameters")] public float offsetX = 0f;
    [FoldoutGroup("Parameters")] public float targetX = 0f;
    [FoldoutGroup("Parameters")] public float slideDuration = 0f;
    [FoldoutGroup("Parameters")] public float slideInterval = 0f;

    public override void InitPopup()
    {
        ResetButtons();
    }

    [Button]
    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        ResetButtons();
        Timing.RunCoroutine(SlideButtons());
    }

    private void ResetButtons()
    {
        for (int i = 0; i < buttonTransforms.Length; ++i)
        {
            RectTransform button = buttonTransforms[i];
            Vector3 position = button.position;
            position.x = offsetX;
            button.position = position;
        }
    }

    private IEnumerator<float> SlideButtons()
    {
        int i = 0;
        while (i < buttonTransforms.Length)
        {
            buttonTransforms[i].DOMoveX(targetX, slideDuration).SetEase(Ease.InOutExpo);
            ++i;
            yield return Timing.WaitForSeconds(slideInterval);
        }
    }

    public override void HidePopup()
    {
        Timing.KillCoroutines();
        gameObject.SetActiveFast(false);
    }
}
