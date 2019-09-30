using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform cameraPos;

    float cameraHeight = 114f;
    //Should change according to the width of respective levels
    //doesn't quite work right now

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos.transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }
}
