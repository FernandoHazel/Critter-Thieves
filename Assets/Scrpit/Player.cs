using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 5f;
    Vector3 movement;

    public float jumpForce = 3f;
    public float maxSpeed = -3;
    public float gravity = -3;
    public bool isGrounded;

    public Vector3 jump;

    public bool Climber;



    // Start is called before the first frame update
    void Start()
    {
    }

    void Movement()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (Climber == true)
            movement.y = Input.GetAxis("Vertical");

        Climber = false;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }



    // Update is called once per frame
    void Update()
    {
        Movement();
        controller.Move(movement * speed * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Climb")
            Climber = true;

        if (other.gameObject.tag == "Floor")
            isGrounded = true;
    }
}

