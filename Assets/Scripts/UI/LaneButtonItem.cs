using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaneButtonItem : MonoBehaviour
{
    public int lane = 0;
    
    public void OnPointerDown(BaseEventData _eventData)
    {
        PopupManager.instance.OnMobileTapDown(lane);
    }

    public void OnPointerUp(BaseEventData _eventData)
    {
        PopupManager.instance.OnMobileTapUp(lane);
    }
}
