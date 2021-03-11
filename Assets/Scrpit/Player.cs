﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;


    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 4f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Movement()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);

        moveVector.x = Input.GetAxis("Horizontal");
        controller.Move(moveVector * Time.deltaTime);
    }


    private void Update()
    {
        Movement();
    }



}
