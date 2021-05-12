using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //TEMPORAL

public class Player : MonoBehaviour
{
    public bool slow = false;
    [SerializeField] UserInterface ui;
    Items item;
    Request request;

    int Hp = 3; //Health
    int MaxHp = 3; //Max Health
    float invencibilityTime = 0;

    public CharacterController controller;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    float savedSpeed;
    float initialSpeed;
    float initialJumpForce;
    float speedPenalization;
    float jumpForcePenalization;

    private float verticalVelocity;
    private float gravity = 9.81f;

    public bool climb = false;
    //bool climbJump = false;

    Vector3 posInicial;
    [SerializeField] Transform posMarcel;
    [SerializeField] GameObject cheeseSpawn;
    [SerializeField] GameObject lastCatch; //El ultimo objeto guardado en la lista food

    List<GameObject> inventory = new List<GameObject>(); //Lista para agarrar y soltar

    public int Score = 0;
    public int queso = 0;
    public int fresa = 0;
    public int nuez = 0;

    int rqQueso = 0; //TEMPORAL
    int rqFresa = 0; //TEMPORAL
    int rqNuez = 0; //TEMPORAL

    public Text contQueso; //TEMPORAL
    public Text contFresa; //TEMPORAL
    public Text contNuez; //TEMPORAL

    public GameObject final;

    bool Boton = false;
    private Vector3 Front;
    [SerializeField] SpriteRenderer[] sprites;

    //public int rq

    void Start()
    {
        //these variables make the slow down work the same without
        //having to go back to the code every time we modify the speed
        //and the jump force
        initialSpeed = speed;
        initialJumpForce = jumpForce;
        speedPenalization = speed * .075f;
        jumpForcePenalization = jumpForce * .075f;

        posInicial = transform.position;
        controller = GetComponent<CharacterController>();
        Front = new Vector3(0, 0, -.3f);
        ui.UpdateHearts(Hp);
    }
    private void Movement() //Movement
    {

        //this is the jump
        if (controller.isGrounded)
        {
            //climbJump = false;
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }

        //this is the climb
        if (climb == true && Input.GetKey(KeyCode.W))
        {
            //climbJump = false;  
            verticalVelocity = Input.GetAxis("Vertical");
        }

        //This is the jump while climbing
        if (climb == true && Input.GetKeyDown(KeyCode.Space))
        {
            //climbJump = true;
            climb = false;
            verticalVelocity = jumpForce;
        }

        else
        {
            //climbJump = false;
            jumpForce = 3;
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //This is the lateral movement
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), verticalVelocity, 0);
        controller.Move(moveVector * speed * Time.deltaTime);
    }

    void GetHurt() //Loose a Life
    {
        if (GameManager.pause) {
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
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].SetActive(true);
        }
        inventory.Clear();

        speed = initialSpeed;
        jumpForce = initialJumpForce;

        queso = 0;
        fresa = 0;
        nuez = 0;
        Score = 0;

        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;

        Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        Debug.Log("Player 1: " + Score);

        ui.UpdateCheese(Score);

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

    public void GrabFood(GameObject other) //Mecanismo para el queso
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Es comida");
            other.gameObject.SetActive(false);
            Items comida = other.GetComponent<Items>();

            if (comida.Tipo == "Fresa")
            {
                Debug.Log("Es Fresa");
                fresa++;
                Score++;
                Debug.Log("Fresas: " + fresa);
            }
            if (comida.Tipo == "Nuez")
            {
                Debug.Log("Es Nuez");
                nuez++;
                Score++;
                Debug.Log("Nueces: " + nuez);
            }
            if (comida.Tipo == "Queso")
            {
                Debug.Log("Es Queso");
                queso++;
                Score++;
                Debug.Log("Queso: " + queso);
            }

            jumpForce = (jumpForce - jumpForcePenalization);
            speed = (speed - speedPenalization);

            Debug.Log("Food: " + Score);
            inventory.Add(other);

            cheeseSpawn = inventory[inventory.Count - 1];

            ui.UpdateCheese(Score);
            Boton = false;
        }
    }

    void dropFood() //Soltar comida
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Score > 0)
            {
                Score--;
                ui.UpdateCheese(Score);
                Debug.Log("Food: " + Score);

                jumpForce = (jumpForce + jumpForcePenalization);
                speed = (speed + speedPenalization);

                var newDrop = Instantiate(cheeseSpawn, posMarcel.position, posMarcel.rotation);
                newDrop.SetActive(true);

                Items tipo = newDrop.GetComponent<Items>();
                if (tipo.Tipo == "Fresa")
                {
                    fresa--;
                    Debug.Log("Fresa: " + fresa);
                }
                else if (tipo.Tipo == "Nuez")
                {
                    nuez--;
                    Debug.Log("Nuez: " + nuez);
                }
                else if (tipo.Tipo == "Queso")
                {
                    queso--;
                    Debug.Log("Queso: " + queso);
                }

                inventory.Remove(cheeseSpawn);
                lastCatch = inventory[inventory.Count - 1];
                cheeseSpawn = lastCatch;
            }
            else
            {
                Debug.Log("No puedes soltar nada porque nada tienes");
            }
        }
    }

    void Nephew()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Score > 0)
            {
                Hp = Hp + Score;

                rqQueso = rqQueso + queso; //TEMPORAL
                rqNuez = rqNuez + nuez; //TEMPORAL
                rqFresa = rqFresa + fresa; //TEMPORAL

                queso = 0;
                nuez = 0;
                fresa = 0;
                Score = 0;

                jumpForce = initialJumpForce;
                speed = initialSpeed;

                ui.UpdateCheese(Score);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + Score);

                inventory.Clear();

                Request1(); //TEMPORAL
            }
        }
    }

    private void OnTriggerStay(Collider other)  //Tags
    {
        if (other.gameObject.tag == "Climb" && controller.isGrounded)
        {
            climb = true;
        }

        if (other.gameObject.tag == "Food")
        {
            GrabFood(other.gameObject);
            Boton = true;
        }

        if (other.gameObject.tag == "Nephew")
        {
            Nephew();
            Boton = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Climb")
        {
            climb = true;
        }

        if (GameManager.pause)
        {
            return;
        }

        if (other.gameObject.tag == "Trap")
        {
            GetHurt();
        }

        if (other.gameObject.tag == "GlueTrap")
        {
            savedSpeed = speed;
            speed = 0.5f;
            //Debug.Log("In" + speed);
            slow = true;
        }
        
        if (other.gameObject.tag == "Food")
        {
            //F.SetActive(true);
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

        if (other.gameObject.tag == "Food")
        {
            Boton = false;
            //F.SetActive(false);
        }

        if (other.gameObject.tag == "GlueTrap")
        {
            speed = savedSpeed;
            //Debug.Log("Out" + speed);
            slow = false;
        }

        if (other.gameObject.tag == "Nephew")
        {
            Boton = false;
            posInicial = transform.position;
            //F.SetActive(false);
        }
    }

    void regulator()
    {
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
        if (transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    void Request1() //TEMPORAL
    {
        if (rqFresa >= 1 && rqNuez >= 2 && rqQueso >= 1)
        {
            Debug.Log("NIVEL COMPLETADO");
            Input.GetKey(KeyCode.P);
            final.SetActive(true);
        }
    }
    private void Update()
    {

        if (GameManager.pause) //Activar la pausa
        {
            return;
        }

        Blink();
        Movement();
        dropFood();
        regulator();

        contNuez.text = "" + nuez;
        contQueso.text = "" + queso;
        contFresa.text = "" + fresa;

    }
}
