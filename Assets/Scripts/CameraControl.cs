using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private  float minX = -10f;
    [SerializeField] private  float maxX = 10f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        float newX = transform.position.x + input * speed * Time.deltaTime;

        newX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
