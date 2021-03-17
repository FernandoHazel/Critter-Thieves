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

    Vector3 posInicial;

    public float Hp = 3; //Health
    int MaxHp = 3; //Max Health
    float invencibilityTime = 0;

    bool Boton = false;

    public float speed = 2.0f;

    public bool Climb = false;

    private Vector3 Front;

    int Key = 0;
    int Score = 0;
    List<GameObject> cheese = new List<GameObject>();
    public SpriteRenderer[] sprites;

     void Start()
    {
        posInicial = transform.position;

        controller = GetComponent<CharacterController>();

        Front = new Vector3(0, 0, -.3f);

        ui.UpdateHearts(Hp);


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
    }

    void GetHurt() //Lose a Life
    {
        if (invencibilityTime > 0)
            return;

        Hp--;

        ui.UpdateHearts(Hp / MaxHp);

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
        cheese.Clear();
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;
        Score = 0;
        //ui.UpdateScore(Score);
        Hp = MaxHp;
        ui.UpdateHearts(1);
        Debug.Log("Player 1: " + Score);

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
                Debug.Log("Food: " + Score);
                cheese.Add(Cheese);
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
        }
    }


    private void OnTriggerStay(Collider other)  //Tags
    {
        Boton = true;
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

    private void OnTriggerExit(Collider other)
    {
        Boton = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            GetHurt();
            Debug.Log("Ouch");
        }
    }


    private void Update()
    {

        Blink();
        Movement();
        ui.UpdateButton(Boton);
    }



}
