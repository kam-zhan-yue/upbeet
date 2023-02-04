using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;
    public FloatReference songPosition;
    public TapBox[] tapBoxes = new TapBox[7];
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.LeftLane1.started += LeftLane1Started;
        playerControls.Player.LeftLane2.started += LeftLane2Started;
        playerControls.Player.LeftLane3.started += LeftLane3Started;
        playerControls.Player.MiddleLane.started += MiddleLaneStarted;
        playerControls.Player.RightLane1.started += RightLane1Started;
        playerControls.Player.RightLane2.started += RightLane2Started;
        playerControls.Player.RightLane3.started += RightLane3Started;
        
        playerControls.Player.LeftLane1.canceled += LeftLane1Cancelled;
        playerControls.Player.LeftLane2.canceled += LeftLane2Cancelled;
        playerControls.Player.LeftLane3.canceled += LeftLane3Cancelled;
        playerControls.Player.MiddleLane.canceled += MiddleLaneCancelled;
        playerControls.Player.RightLane1.canceled += RightLane1Cancelled;
        playerControls.Player.RightLane2.canceled += RightLane2Cancelled;
        playerControls.Player.RightLane3.canceled += RightLane3Cancelled;
        playerControls.Enable();
        for (int i = 0; i < tapBoxes.Length; ++i)
        {
            if(tapBoxes[i] != null)
                tapBoxes[i].Init(scoreController);
        }
    }

    private void LeftLane1Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[0] == null)
            return;
        tapBoxes[0].TapDown(songPosition);
    }

    private void LeftLane2Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[1] == null)
            return;
        tapBoxes[1].TapDown(songPosition);
    }

    private void LeftLane3Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[2] == null)
            return;
        tapBoxes[2].TapDown(songPosition);
    }

    private void MiddleLaneStarted(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[3] == null)
            return;
        tapBoxes[3].TapDown(songPosition);
    }

    private void RightLane1Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[4] == null)
            return;
        tapBoxes[4].TapDown(songPosition);
    }

    private void RightLane2Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[5] == null)
            return;
        tapBoxes[5].TapDown(songPosition);
    }
    
    private void RightLane3Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[6] == null)
            return;
        tapBoxes[6].TapDown(songPosition);
    }


    private void LeftLane1Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[0] == null)
            return;
        tapBoxes[0].TapUp();
    }

    private void LeftLane2Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[1] == null)
            return;
        tapBoxes[1].TapUp();
    }

    private void LeftLane3Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[2] == null)
            return;
        tapBoxes[2].TapUp();
    }

    private void MiddleLaneCancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[3] == null)
            return;
        tapBoxes[3].TapUp();
    }

    private void RightLane1Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[4] == null)
            return;
        tapBoxes[4].TapUp();
    }

    private void RightLane2Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[5] == null)
            return;
        tapBoxes[5].TapUp();
    }
    
    private void RightLane3Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[6] == null)
            return;
        tapBoxes[6].TapUp();
    }
    
    private void OnDestroy()
    {
        playerControls.Player.LeftLane1.started -= LeftLane1Started;
        playerControls.Player.LeftLane2.started -= LeftLane2Started;
        playerControls.Player.LeftLane3.started -= LeftLane3Started;
        playerControls.Player.MiddleLane.started -= MiddleLaneStarted;
        playerControls.Player.RightLane1.started -= RightLane1Started;
        playerControls.Player.RightLane2.started -= RightLane2Started;
        playerControls.Player.RightLane3.started -= RightLane3Started;
        
        playerControls.Player.LeftLane1.canceled -= LeftLane1Cancelled;
        playerControls.Player.LeftLane2.canceled -= LeftLane2Cancelled;
        playerControls.Player.LeftLane3.canceled -= LeftLane3Cancelled;
        playerControls.Player.MiddleLane.canceled -= MiddleLaneCancelled;
        playerControls.Player.RightLane1.canceled -= RightLane1Cancelled;
        playerControls.Player.RightLane2.canceled -= RightLane2Cancelled;
        playerControls.Player.RightLane3.canceled -= RightLane3Cancelled;
        playerControls.Disable();
    }
}
