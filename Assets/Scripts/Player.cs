﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    //[SerializeField] Platform platformScript;
    [SerializeField] private Animator animator;
    [SerializeField] UserInterface ui;
    private CharacterController controller;
    private float verticalVelocity;
    private float gravity = 9.81f;
    [SerializeField] float jumpForce;

    [SerializeField] Transform posMarcel;
    [SerializeField] GameObject cheeseSpawn;

    Vector3 posInicial;

    int Hp = 3; //Health
    int MaxHp = 3; //Max Health
    float invencibilityTime = 0;

    bool Boton = false;

    [SerializeField] float speed;
    float savedSpeed;

    bool climb = false;
    bool climbJump = false;

    private Vector3 Front;

    int Key = 0;
    int Score = 0;
    int queso = 0;
    List<GameObject> cheese = new List<GameObject>();
    [SerializeField] SpriteRenderer[] sprites;
    //public GameObject F;
    float initialSpeed;
    float initialJumpForce;
    float speedPenalization;
    float jumpForcePenalization;

    void Start()
    {
        //these variables make the slow down work the same without
        //having to go back to the code every time we modify the speed
        //and the jump force
        //double a =1;
        //double b =6;
        initialSpeed = speed;
        initialJumpForce = jumpForce;
        speedPenalization = speed * .1666666f;
        jumpForcePenalization = jumpForce * .1666666f;
        //Debug.Log(initialSpeed);
        //Debug.Log(speedPenalization);

        posInicial = transform.position;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Front = new Vector3(0, 0, -.3f);
        ui.UpdateHearts(Hp);
    }


    private void Movement() //Movement
    {
        
        //this is the jump
        if (controller.isGrounded)
        {
            climbJump = false;
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }

        //this is the climb
        if (climb == true && Input.GetKey(KeyCode.W))
        {
            climbJump = false;  
            //Debug.Log("must climb");
            verticalVelocity = Input.GetAxis("Vertical");
        }

        //This is the jump while climbing
        if (climb == true && Input.GetKeyDown(KeyCode.Space))
        {
            climbJump = true;
            climb = false;
            verticalVelocity = jumpForce;
        }

        /*if (Input.GetAxisRaw("Horizontal") != 0)
        {
            climb = false;
            climbJump = false;
            animator.SetBool("Run", true);
        }*/

        else
        {
            //animator.SetBool("Run", false);
            climbJump = false;
            jumpForce=3;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        //This is the lateral movement
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), verticalVelocity, 0);
        controller.Move(moveVector * speed * Time.deltaTime);

    }

        void ExitLevel()
    {
        if (Input.GetKeyDown(KeyCode.F)) //Ahora para salir también es necesario presionar F
        {
            if(Key == 3)
            {
                Debug.Log("Completaste el nivel!");
            }
            else
            {
                Debug.Log("Aún no tienes todas las llaves, regresa después");
            }
        }
    }

    void GetHurt() //Loose a Life
    {
        if (GameManager.pause){
            return;
        }

        if (invencibilityTime > 0)
            return;
        Hp--;

        ui.UpdateHearts(Hp);


        transform.position = posInicial;

        invencibilityTime = 2;

        if (Hp <= 0)
        {
            Die();
        }
    }

    public void Die() //Reset Everything
    {
        

        for (int i = 0; i < cheese.Count; i++)
        {
            cheese[i].SetActive(true);
        }
        speed = initialSpeed;
        Score = 0;
        cheese.Clear();
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;
        Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        Debug.Log("Player 1: " + Score);
        queso = 0;
        jumpForce = initialJumpForce;
        Key = 0;
        ui.UpdateStillson(Key);
        ui.UpdateCheese(queso);

    }

    void Blink() //Invencibility
    {
        invencibilityTime -= Time.deltaTime;

        if (invencibilityTime > 0)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].enabled = !sprites[i].enabled;
            }
        }
        else
        {
            if (!sprites[0].enabled)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].enabled = true;
                }
            }
        }
    }

    public void GrabCheese(GameObject Cheese) //Mecanismo para el queso
    {
       
        if (Input.GetKeyDown(KeyCode.F))
        {
            Score++;
            queso++;
            //jumpForce = (jumpForce - (Score * .1f));
            //speed = (speed - (Score * .1f));

            jumpForce = (jumpForce - jumpForcePenalization);
            speed = (speed - speedPenalization);

            Cheese.gameObject.SetActive(false);
            Debug.Log("Food: " + Score);
            cheese.Add(Cheese);
            
            ui.UpdateCheese(queso);
            Boton = false;

        }
    }

    void dropCheese() //Soltar comida
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (queso > 0)
            {
                queso--;
                Score--;

                //jumpForce = (jumpForce + (Score * .1f));
                //speed = (speed + (Score * .1f));

                jumpForce = (jumpForce + jumpForcePenalization);
                speed = (speed + speedPenalization);

                Instantiate(cheeseSpawn, posMarcel.position, posMarcel.rotation);
                Debug.Log("Food: " + Score);
               
                ui.UpdateCheese(queso);
            }
            else
            {
                Debug.Log("No puedes soltar nada porque nada tienes");
            }
        }
    }

    void GrabKey(GameObject Stillson) //Mecanismo para la llave
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Key++;

            //ui.UpdateScore(Score);
            Stillson.gameObject.SetActive(false);
            Debug.Log("Stillson Parts: " + Key);
            cheese.Add(Stillson);
            ui.UpdateStillson(Key);
            Boton = false;

        }
    }

    void Nephew()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (queso > 0)
            {
                Hp = Hp + (queso);
                queso = 0;
                Score = 0;
                jumpForce = initialJumpForce;
                speed = initialSpeed;

                ui.UpdateCheese(queso);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + Score);
            }

            /*
            else
            {
                queso--;
                Score--;
                jumpForce = (jumpForce + (Score * .1f));
                speed = (speed + (Score * .1f));

                //Agregar que por cadaqueso que entregas recuperas velocidad y salto
                ui.UpdateCheese(queso);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + Score);
            }
            */
        }
    }

    private void OnTriggerStay(Collider other)  //Tags
    {
        if (other.gameObject.tag == "Climb" && controller.isGrounded)
        {
            climb = true;
        }
        
        if (other.gameObject.tag == "Cheese")
        {
            GrabCheese(other.gameObject);
            Boton = true;
            
        }

        if (other.gameObject.tag == "Stillson")
        {
            GrabKey(other.gameObject);
            Boton = true;
        }

        if (other.gameObject.tag == "Nephew")
        {
            Nephew();
            Boton = true;
        }

        if (other.gameObject.tag == "Valve")
        {
            ExitLevel();
            Boton = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.pause)
        {
            return;
        }
        

        if (other.gameObject.tag == "Climb")
        {
            climb = false;
            //Debug.Log("no trepo");
        }
        if (other.gameObject.tag == "Cheese")
        {
            GrabCheese(other.gameObject);
            Boton = false;
            //F.SetActive(false);
        }

        if (other.gameObject.tag == "Stillson")
        {
            GrabKey(other.gameObject);
            Boton = false;
        }

        if (other.gameObject.tag == "GlueTrap")
        {
            speed = savedSpeed;
            Debug.Log("Out" + speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Climb")
        {
            climb = true;
            //Debug.Log("trepo");
        }

        if (GameManager.pause)
        {
            return;
        }

        if (other.gameObject.tag == "Trap")
        {
            GetHurt();
            //Debug.Log("Ouch");
        }

        if (other.gameObject.tag == "GlueTrap")
        {
            savedSpeed = speed;
            speed = 0.5f;
            Debug.Log("In" + speed);
        }

        if (other.gameObject.tag == "Cheese")
        {
            //F.SetActive(true);
        }
        
    }


    private void Update()
    {
        //pause
        if (GameManager.pause)
        {
            return;
        }

        Blink();
        Movement();
        dropCheese();
        //platformScript.climbHere = climb;
        //platformScript.climbJumpHere = climbJump;
            //animator.SetBool("Run", true);
        if (Hp > 3)
        {
            Hp = 3;
        }
        if (speed < .1f)
        {
            speed = speedPenalization;
        }
        if (jumpForce < 1)
        {
            jumpForce = jumpForcePenalization;
        }
        if (speed > initialSpeed)
        {
            speed = initialSpeed;
        }
        if (jumpForce < initialJumpForce)
        {
            jumpForce = initialJumpForce;
        }
    }



}
