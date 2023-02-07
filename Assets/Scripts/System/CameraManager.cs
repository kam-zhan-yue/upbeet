using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera touchCamera;
    public Camera mainCamera;
    
    public Vector3 pos = new Vector3(200, 200, 0);
    public Vector3 target = new Vector3();


    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 relativePos = target - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
        Ray ray = touchCamera.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, rotation*ray.direction * 10, Color.yellow);
    }
    public Vector3 ScreenToWorldMain(Vector3 _screenPosition)
    {
        return Vector3.zero;
    }
}
