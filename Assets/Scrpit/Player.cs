using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;


    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 4f;

    public float speed = 1.0f;

    public bool Climb = false;

    private Vector3 Front;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Front = new Vector3(0, 0, -.3f);

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

        if (Climb == true)
        {
            //transform.position = Vector3.Lerp(transform.position,  , speed * Time.deltaTime);

        }
        else
        {
            moveVector.z = -.13f;
        }

        Climb = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Climb")
        {
            Climb = true;
        }
    }


    private void Update()
    {
        Movement();
    }



}
