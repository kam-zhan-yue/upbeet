using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Popup : MonoBehaviour
{
    [NonSerialized, ShowInInspector, ReadOnly]
    public bool isAnimating = false;
    
    [NonSerialized, ShowInInspector, ReadOnly]
    public bool isShowing = false;
    
    public Action<Popup> onCloseButtonClicked = null;

    private void Awake()
    {
        InitPopup();
    }

    protected abstract void InitPopup();

    public virtual void ShowPopup()
    {
        isShowing = true;
    }

    public virtual void HidePopup()
    {
        isShowing = false;
    }
    
    public virtual void DoneShowing()
    {
        isAnimating = false;
    }

    public virtual void DoneHiding()
    {
        isAnimating = false;
        isShowing = false;
    }

    public virtual void CloseButtonClicked()
    {
        onCloseButtonClicked?.Invoke(this);
    }

    public virtual void EscapeButtonClicked()
    {
        CloseButtonClicked();
    }
}