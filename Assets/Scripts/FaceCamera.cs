using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        Vector3 cameraDirection = mainCamera.transform.forward;
        cameraDirection.y = 0;

        transform.forward = cameraDirection;
    }
}
