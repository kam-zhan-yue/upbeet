using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;
    public FloatReference songPosition;
    public TapBox[] tapBoxes = new TapBox[7];
    private Camera touchCamera;
    private PlayerControls playerControls;
    private bool hasCamera = false;

    private void Awake()
    {
        // main = Camera.main;
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
        
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnFingerDown;

        for (int i = 0; i < tapBoxes.Length; ++i)
        {
            if(tapBoxes[i] != null)
                tapBoxes[i].Init(scoreController);
        }
    }

    public void OnMobileTapDown(int _lane)
    {
        if (_lane == 1 || _lane == 2 || _lane == 4 || _lane == 5)
        {
            if (tapBoxes[_lane] != null)
                tapBoxes[_lane].TapDown(songPosition);
        }
    }

    public void OnMobileTapUp(int _lane)
    {
        if (_lane == 1 || _lane == 2 || _lane == 4 || _lane == 5)
        {
            if (tapBoxes[_lane] != null)
                tapBoxes[_lane].TapUp();
        }
    }

    private void Start()
    {
        // touchCamera = CameraManager.instance.touchCamera;
        hasCamera = true;
    }

    private void OnFingerDown(Finger _finger)
    {
        if (!hasCamera)
            return;
        // Vector2 screenPosition = _finger.screenPosition;
        // Vector2 worldPoint = touchCamera.ScreenToWorldPoint(screenPosition);
        // Vector3 customPoint = CameraManager.instance.ScreenToWorldMain(screenPosition);
        // Debug.Log(screenPosition + " "+worldPoint);
        // RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        // if (hit.collider == null)
        //     return;
        // if(hit.collider.gameObject == null)
        //     return;
        // if (hit.collider.gameObject.TryGetComponent(out TapBox tapBox))
        // {
        //     tapBox.TapDown(songPosition);
        // }
        // Vector2 screenPosition = _finger.screenPosition;
        // CameraManager.instance.pos = screenPosition;
        // Debug.Log(screenPosition);
        // Ray ray = touchCamera.ScreenPointToRay(screenPosition);
        // RaycastHit raycastHit;
        // if (Physics.Raycast(ray, out raycastHit))
        // {
        //     Debug.Log ("We have a hit!");
        // }
    }

//     private void Update()
//     {
//         if (!hasCamera)
//             return;
//         // playerControls.Player.Touch.po;
//         // ReadOnlyArray<TouchControl> touchControls = Touchscreen.current.touches;
//         // if (touchControls.Count > 0)
//         // {
//         //     Debug.Log("Touch");
//         //     for (int i = 0; i < touchControls.Count; ++i)
//         //     {
//         //         Vector2 touchPos = touchControls[i].position.ReadValue();
//         //         // Debug.Log(touchPos);
//         //         Vector2 worldPoint = touchCamera.ScreenToWorldPoint(touchPos);
//         //         // Debug.Log(worldPoint);
//         //         RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
//         //         if (hit.collider == null)
//         //             continue;
//         //         if(hit.collider.gameObject == null)
//         //             continue;
//         //         if (hit.collider.gameObject.TryGetComponent(out TapBox tapBox))
//         //         {
//         //             // TouchPhase phase = touchControls[i].phase.ReadValue();
//         //             // if (Input.touches[i].phase == TouchPhase.Began)
//         //             // {
//         //             //     Debug.Log("Tap Down");
//         //             //     tapBox.TapDown(songPosition);
//         //             // }
//         //             // else if(Input.touches[i].phase == TouchPhase.Canceled)
//         //             // {
//         //             //     Debug.Log("Tap Up");
//         //             //     tapBox.TapUp();
//         //             // }
//         //         }
//         //     }
//         // }
// #if UNITY_EDITOR
//         // if(Input.GetMouseButtonDown(0))
//         // {
//         //     // Ray2D ray = main.ScreenPointToRay(Input.mousePosition);
//         //     Vector2 worldPoint = touchCamera.ScreenToWorldPoint(Input.mousePosition);
//         //     // Debug.Log(worldPoint);
//         //     RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
//         //     if (hit.collider == null)
//         //         return;
//         //     if(hit.collider.gameObject == null)
//         //         return;
//         //     if (hit.collider.gameObject.TryGetComponent(out TapBox tapBox))
//         //     {
//         //         tapBox.TapDown(songPosition);
//         //     }
//         // }
// #endif
//     }

    private void LeftLane1Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[0] == null || !isActiveAndEnabled)
            return;
        tapBoxes[0].TapDown(songPosition);
    }

    private void LeftLane2Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[1] == null || !isActiveAndEnabled)
            return;
        tapBoxes[1].TapDown(songPosition);
    }

    private void LeftLane3Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[2] == null || !isActiveAndEnabled)
            return;
        tapBoxes[2].TapDown(songPosition);
    }

    private void MiddleLaneStarted(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[3] == null || !isActiveAndEnabled)
            return;
        tapBoxes[3].TapDown(songPosition);
    }

    private void RightLane1Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[4] == null || !isActiveAndEnabled)
            return;
        tapBoxes[4].TapDown(songPosition);
    }

    private void RightLane2Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[5] == null || !isActiveAndEnabled)
            return;
        tapBoxes[5].TapDown(songPosition);
    }
    
    private void RightLane3Started(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[6] == null || !isActiveAndEnabled)
            return;
        tapBoxes[6].TapDown(songPosition);
    }


    private void LeftLane1Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[0] == null || !isActiveAndEnabled)
            return;
        tapBoxes[0].TapUp();
    }

    private void LeftLane2Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[1] == null || !isActiveAndEnabled)
            return;
        tapBoxes[1].TapUp();
    }

    private void LeftLane3Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[2] == null || !isActiveAndEnabled)
            return;
        tapBoxes[2].TapUp();
    }

    private void MiddleLaneCancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[3] == null || !isActiveAndEnabled)
            return;
        tapBoxes[3].TapUp();
    }

    private void RightLane1Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[4] == null || !isActiveAndEnabled)
            return;
        tapBoxes[4].TapUp();
    }

    private void RightLane2Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[5] == null || !isActiveAndEnabled)
            return;
        tapBoxes[5].TapUp();
    }
    
    private void RightLane3Cancelled(InputAction.CallbackContext _callbackContext)
    {
        if (tapBoxes[6] == null || !isActiveAndEnabled)
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
