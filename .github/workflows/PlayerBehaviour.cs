/* Copyright by Mideky-hub on GitHub on the 2021.
 * 
 * No claim can be made out of this code by the fact that its for private usage, the only goal of the code is to complete my End Year Project.
 * Any modification provided in this code has to be verified by me and is going into my intellectual protection.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Unity librairies documentation on : www.docs.unity3d.com
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Camera playerCamera;

    // We import the librairies from "UnityEngine".
    CharacterController characterController;
    Vector3 moveDirection;

    // Setting speed.
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 15f;

    // Setting physics.
    public float jumpForce = 6.5f;
    public float gravity = 20f;

    // Setting base state.
    public bool isRunning = false;

    // Setting Rotation possible variation.
    public float rotationX = 0;
    public float rotationSpeed = 2.0f;
    public float rotationXLimit = 45.0f;

    void Start()
    {
        // We set the cursor invisible.
        Cursor.visible = false;
        // We can extrod the component from the "CharacterController" libraires.
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Getting axis postition in order to calculate and move the player arround the place.
        float SpeedZ = Input.GetAxis("Vertical");
        float SpeedX = Input.GetAxis("Horizontal");

        // To the top, like jumping "moveDirection.y" is from Vector3, see the "www.docs.unity3d.com"
        float SpeedY = moveDirection.y;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //If Left Shift is pressed we will run.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Setting isRunning for the next statement.
            isRunning = true;
        }
        else
        {
            // **
            isRunning = false;
        }

        //We need do set for the isRunning statement after.
        if (isRunning)
        {
            // We move the player arround if his running, so we multiply our variable w/ the move speed of the player.
            SpeedX = SpeedX * runningSpeed;
            SpeedZ = SpeedZ * runningSpeed;
        }
        else
        {
            // As same as we multiply the player speed by the axis
            SpeedX = SpeedX * walkingSpeed;
            SpeedZ = SpeedZ * walkingSpeed;
        }

        // As we have our values (SpeedZ / SpeedX), we can now calculate the moveDirection to move the character through the map.
        moveDirection = forward * SpeedZ + right * SpeedX;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            // As we have a jumpForce element.
            moveDirection.y = jumpForce;
        }
        else
        {
            // As we calculated the SpeedY value we can use it to move on the Y axe.
            moveDirection.y  = SpeedY;
        }

        if (!characterController.isGrounded)
        {
            //Time.deltaTime return the value of the time elapsed since the last frames appeared.
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // We move the character, as we have all the value.
        characterController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }
}