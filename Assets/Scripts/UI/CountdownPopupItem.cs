using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class CountdownPopupItem : MonoBehaviour
{
    [FoldoutGroup("UI Objects")] public TMP_Text countdownText;
    [FoldoutGroup("Parameters")] public float countdownTime = 0f;

    private void Awake()
    {
        gameObject.SetActiveFast(false);
    }

    [Button]
    public void Activate(Action _onComplete = null)
    {
        gameObject.SetActiveFast(true);
        DOVirtual.Float(countdownTime, 0, countdownTime, OnCountdownUpdate).SetUpdate(true)
            .SetEase(Ease.Linear).OnComplete(() =>
        {
            _onComplete?.Invoke();
        });
    }

    private void OnCountdownUpdate(float _time)
    {
        gameObject.SetActiveFast(true);
        countdownText.text = _time.ToString("F1");
    }
}
