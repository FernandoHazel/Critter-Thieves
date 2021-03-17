using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public UserInterface ui;
    private CharacterController controller;


    private float verticalVelocity;
    private float gravity = 14.0f;
    public float jumpForce = 4f;

    Vector3 posInicial;

    int Hp = 3; //Health
    int MaxHp = 3; //Max Health
    float invencibilityTime = 0;

    bool Boton = false;

    public float speed = 2.0f;

    public bool Climb = false;

    private Vector3 Front;

    int Key = 0;
    int Score = 0;
    int queso = 0;
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
            verticalVelocity = Input.GetAxis("Vertical");
        }
      
    }

    void GetHurt() //Lose a Life
    {
        Hp--;

        ui.UpdateHearts(Hp);

        if (invencibilityTime > 0)
            return;


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
        Score = 0;
        cheese.Clear();
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;
        Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        Debug.Log("Player 1: " + Score);
        queso = 0;
        jumpForce = 4f;
        Key = 0;
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

    void GrabCheese(GameObject Cheese) //Mecanismo para el queso
    {
       

        if (jumpForce > 2)
            {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Score++;

                jumpForce = (jumpForce - (Score * .1f));

               
                Cheese.gameObject.SetActive(false);
                Debug.Log("Food: " + Score);
                cheese.Add(Cheese);
                queso++;
                ui.UpdateCheese(queso);
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
            //Debug.Log("trepo");
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

        if (other.gameObject.tag == "Climb")
        {
            Climb = false;
            //Debug.Log("no trepo");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            GetHurt();
            //Debug.Log("Ouch");
        }
    }


    private void Update()
    {

        Blink();
        Movement();
        ui.UpdateButton(Boton);
    }



}
