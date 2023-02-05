using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActiveFast(true);
    }

    public void Hide()
    {
        gameObject.SetActiveFast(false);
    }
}
