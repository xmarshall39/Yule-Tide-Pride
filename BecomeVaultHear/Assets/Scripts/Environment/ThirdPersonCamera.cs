using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test script that provides camera movement with light restrictions
public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0f;
    private const float Y_ANGLE_MAX = 50.0f;
    public Transform lookAt, camTransform;
    
    private Camera cam;

    public float distance = 10.0f; //Dist from camera to player
    private float currX, currY;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;

    private void Start()
    {
        camTransform = this.transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currX += Input.GetAxis("Mouse X") * sensitivityX;
        currY += Input.GetAxis("Mouse Y") * sensitivityY;

        currY = Mathf.Clamp(currY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    //Move camera after player
    private void LateUpdate()
    {
        //Position the camera behind the player
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rot = Quaternion.Euler(currY, currX, 0);
        camTransform.position = lookAt.position + rot * dir;

        //Keep eyes on the player
        camTransform.LookAt(lookAt.position);
    }
}
