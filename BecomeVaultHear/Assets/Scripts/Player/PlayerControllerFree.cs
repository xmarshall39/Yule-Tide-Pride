using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Testing script that uses free player movement.
//Use until final script is created. To test environmental reactions to player
public class PlayerControllerFree : MonoBehaviour
{

    CharacterController controller;
    public float speed = 3;
    public float jumpForce = 0.1f;
    public float G = 1;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {

        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        if (controller.isGrounded)
        {
            verticalVelocity = -G * Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
                print($"After Jump: {verticalVelocity}");
            }
        }

        //Gain speed upon fall
        else
        {
            verticalVelocity -= G * Time.deltaTime;
        }

        controller.Move(new Vector3(x, verticalVelocity, z));
    }
}
