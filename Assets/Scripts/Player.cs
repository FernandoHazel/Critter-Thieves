using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public UserInterface ui;
    private CharacterController controller;
    private float verticalVelocity;
    private float gravity = 14.0f;
    public float jumpForce = 2f;

    public Transform posMarcel;
    public GameObject cheeseSpawn;

    Vector3 posInicial;

    int Hp = 3; //Health
    int MaxHp = 3; //Max Health
    float invencibilityTime = 0;

    bool Boton = false;

    public float speed = 3.0f;

    public bool Climb = false;

    private Vector3 Front;

    int Key = 0;
    float Score = 0;
    int queso = 0;
    List<GameObject> cheese = new List<GameObject>();
    public SpriteRenderer[] sprites;
    //public GameObject F;

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

        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal") - queso, verticalVelocity, 0);
        controller.Move(moveVector * speed * Time.deltaTime);

        if (Climb == true && Input.GetKey(KeyCode.W))
        {
            verticalVelocity = Input.GetAxis("Vertical");
        }
    }

        void ExitLevel()
    {
        if (Key == 3)
        {
            Debug.Log("Completaste el nivel!");
        }
        else
        {
            Debug.Log("Aún no tienes todas las llaves, regresa después");
        }
    }

    void GetHurt() //Lose a Life
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
        speed = 3;
        Score = 0;
        cheese.Clear();
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;
        Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        Debug.Log("Player 1: " + Score);
        queso = 0;
        jumpForce = 2f;
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

            jumpForce = (jumpForce - (Score * .1f));

            
            Cheese.gameObject.SetActive(false);
            Debug.Log("Food: " + Score);
            cheese.Add(Cheese);
            queso++;
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
                Score--;

                jumpForce = (jumpForce - (Score * .1f));


                Instantiate(cheeseSpawn, posMarcel.position, posMarcel.rotation);
                Debug.Log("Food: " + Score);
                queso--;
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
            if (queso/2 == 0)
            {
                Hp = Hp + (queso / 2);
                queso = 0;
                jumpForce = (jumpForce - (Score * .1f));

                ui.UpdateCheese(queso);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + Score);
            }
            else
            {
                Hp = Hp + (queso / 2);
                queso--;
                //Agregar que por cadaqueso que entregas recuperas velocidad y salto
                ui.UpdateCheese(queso);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + Score);

            }

        }
    }


    private void OnTriggerStay(Collider other)  //Tags
    {
        
        if (other.gameObject.tag == "Climb")
        {
            Climb = true;
            //Debug.Log("trepo");
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
        if (GameManager.pause){
            return;
        }
        

        if (other.gameObject.tag == "Climb")
        {
            Climb = false;
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
            speed = 3.0f;
            Debug.Log("Out" + speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.pause){
            return;
        }

        if (other.gameObject.tag == "Trap")
        {
            GetHurt();
            //Debug.Log("Ouch");
        }

        if (other.gameObject.tag == "GlueTrap")
        {
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
        if (GameManager.pause){
            return;
        }

        Blink();
        Movement();
        dropCheese();

    }



}
