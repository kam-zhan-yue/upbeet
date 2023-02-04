using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEventTrigger : MonoBehaviour
{
    [FoldoutGroup("Parameters")] public Color defaultColour;
    [FoldoutGroup("Parameters")] public Color pointerEnterColour;
    [FoldoutGroup("Parameters")] public Color pointerDownColour;
    [FoldoutGroup("Parameters")] public Color pointerUpColour;
    
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void OnPointerEnter(BaseEventData _eventData)
    {
        image.color = pointerEnterColour;
    }
    
    public void OnPointerDown(BaseEventData _eventData)
    {
        image.color = pointerDownColour;
    }

    public void OnPointerUp(BaseEventData _eventData)
    {
        image.color = pointerUpColour;
    }
    
    public void OnPointerExit(BaseEventData _eventData)
    {
        image.color = defaultColour;
    }
}
