using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.LeftLane1.started += LeftLane1Started;
        playerControls.Player.LeftLane2.started += LeftLane2Started;
        playerControls.Player.LeftLane3.started += LeftLane3Started;
        playerControls.Player.RightLane1.started += RightLane1Started;
        playerControls.Player.RightLane2.started += RightLane2Started;
        playerControls.Player.RightLane3.started += RightLane3Started;
        playerControls.Enable();
    }

    private void LeftLane1Started(InputAction.CallbackContext _callbackContext)
    {
        
    }

    private void LeftLane2Started(InputAction.CallbackContext _callbackContext)
    {
        
    }

    private void LeftLane3Started(InputAction.CallbackContext _callbackContext)
    {
        
    }

    private void RightLane1Started(InputAction.CallbackContext _callbackContext)
    {
        
    }

    private void RightLane2Started(InputAction.CallbackContext _callbackContext)
    {
        
    }
    private void RightLane3Started(InputAction.CallbackContext _callbackContext)
    {
        
    }
}
