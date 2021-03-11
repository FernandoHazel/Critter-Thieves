using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public UserInterface ui;
    private CharacterController controller;


    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 4f;

    public float speed = 2.0f;

    public bool Climb = false;

    private Vector3 Front;

    int Key = 0;
    int Score = 0;
    List<GameObject> cheese = new List<GameObject>();


    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Front = new Vector3(0, 0, -.3f);

    }


    private void Movement() //Movement
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
            //if (Input.GetKey(KeyCode.W))
               // moveVector.y = speed;
        }
        else
        {
            moveVector.z = -.13f;
        }

        Climb = false;

        Debug.Log(speed);
    }

    void GrabCheese(GameObject Cheese) //Mecanismo para el queso
    {
        if (jumpForce > 2)
            {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Score++;

                jumpForce = (jumpForce - (Score * .2f));

                //ui.UpdateScore(Score);
                Cheese.gameObject.SetActive(false);
                Debug.Log("Player 1: " + Score);
                cheese.Add(Cheese);
            }
        }
    }

    void GrabKey(GameObject Stillson)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Key++;

            //ui.UpdateScore(Score);
            Stillson.gameObject.SetActive(false);
            Debug.Log("Player 1: " + Key);
            cheese.Add(Stillson);
        }
    }


    private void OnTriggerStay(Collider other)  //Tags
    {
        if (other.gameObject.tag == "Climb")
        {
            Climb = true;
            Debug.Log("trepo");
        }


        if (other.gameObject.tag == "Cheese")
        {
            GrabCheese(other.gameObject);

        }

        if (other.gameObject.tag == "Stillson")
        {
            GrabKey(other.gameObject);
        }
    }


    private void Update()
    {
        Movement();
        Debug.Log("jumpForce: " + jumpForce);
    }



}
