using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //TEMPORAL

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    float canClimbCounter = 0.5f;
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

    //List<GameObject> inventory = new List<GameObject>(); //Lista para agarrar y soltar

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

    public Transform BossCam;

    //public int rq

    void Start()
    {
        transform.position = playerData.posSaved;
        //these variables make the slow down work the same without
        //having to go back to the code every time we modify the speed
        //and the jump force
        initialSpeed = speed;
        initialJumpForce = jumpForce;
        speedPenalization = speed * .075f;
        jumpForcePenalization = jumpForce * .075f;

        
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
        if (climb == true && Input.GetKey(KeyCode.W) || climb == true && Input.GetKey(KeyCode.UpArrow))
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
            canClimbCounter = 0.5f;
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

        invencibilityTime = 2;

        if (Hp <= 0)
        {
            Die();
        }
    }

    public void Die() //Reset Everything
    {
        for (int i = 0; i < playerData.inventory.Count; i++)
        {
            playerData.inventory[i].SetActive(true);
        }
        playerData.inventory.Clear();
        playerData.mochila.Clear();

        speed = initialSpeed;
        jumpForce = initialJumpForce;
        playerData.queso = 0;
        playerData.fresa = 0;
        playerData.nuez = 0;
        playerData.Score = 0;
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;

        Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        Debug.Log("Player 1: " + playerData.Score);

        ui.UpdateCheese(playerData.Score);

    }

    public void CamRun()
    {
        //Debug.Log(transform.position.x - BossCam.position.x);

        if (transform.position.x - BossCam.position.x <= -15)
        {
            Die();
            Debug.Log("You died");
        }
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
            jumpForce = (jumpForce - jumpForcePenalization);
            speed = (speed - speedPenalization);

            //Debug.Log("Food: " + playerData.Score);
            if(other != null)
            {
                playerData.inventory.Add(other);
                playerData.mochila.Add(other.GetComponent<Items>().ID, other);
            }
            
            cheeseSpawn = other;

            ui.UpdateCheese(playerData.Score);
            Boton = false;
        }
    }

    void dropFood() //Soltar comida
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerData.Score > 0)
            {
                playerData.Score--;
                ui.UpdateCheese(playerData.Score);
                //Debug.Log("Food: " + playerData.Score);

                jumpForce = (jumpForce + jumpForcePenalization);
                speed = (speed + speedPenalization);

                cheeseSpawn.SetActive(true);
                cheeseSpawn.transform.position = transform.position;

                Items tipo = cheeseSpawn.GetComponent<Items>();
                if (tipo.Tipo == "Fresa")
                {
                    cheeseSpawn.GetComponent<Items>().saveItem(cheeseSpawn.GetComponent<Items>().ID, transform.localPosition);
                    playerData.fresa--;
                    //Debug.Log("Fresa: " + playerData.fresa);
                }
                else if (tipo.Tipo == "Nuez")
                {
                    cheeseSpawn.GetComponent<Items>().saveItem(cheeseSpawn.GetComponent<Items>().ID, transform.localPosition);
                    playerData.nuez--;
                    //Debug.Log("Nuez: " + playerData.nuez);
                }
                else if (tipo.Tipo == "Queso")
                {
                    cheeseSpawn.GetComponent<Items>().saveItem(cheeseSpawn.GetComponent<Items>().ID, transform.localPosition);
                    playerData.queso--;
                    //Debug.Log("Queso: " + playerData.queso);
                }

                playerData.inventory.Remove(cheeseSpawn);
                playerData.mochila.Remove(cheeseSpawn.GetComponent<Items>().ID);
                if (playerData.inventory.Count != 0)
                {
                    cheeseSpawn = playerData.inventory[playerData.inventory.Count - 1];
                    cheeseSpawn = playerData.mochila[cheeseSpawn.GetComponent<Items>().ID];
                }
    
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
            if (playerData.Score > 0)
            {
                Hp = Hp + playerData.Score;

                rqQueso = rqQueso + playerData.queso; //TEMPORAL
                rqNuez = rqNuez + playerData.nuez; //TEMPORAL
                rqFresa = rqFresa + playerData.fresa; //TEMPORAL

                playerData.queso = 0;
                playerData.nuez = 0;
                playerData.fresa = 0;
                playerData.Score = 0;

                jumpForce = initialJumpForce;
                speed = initialSpeed;

                ui.UpdateCheese(playerData.Score);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + playerData.Score);

                playerData.inventory.Clear();

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
        canClimbCounter -= Time.deltaTime;
        //Debug.Log(canClimbCounter);
        if (other.gameObject.tag == "Climb")
        {
            if (canClimbCounter <= 0)
            {
                climb = true;
                canClimbCounter = 0.5f;
            }
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
        playerData.posSaved = transform.position;
        //CamRun();

        if (GameManager.pause) //Activar la pausa
        {
            return;
        }

        Blink();
        Movement();
        dropFood();
        regulator();

        contNuez.text = "" + playerData.nuez;
        contQueso.text = "" + playerData.queso;
        contFresa.text = "" + playerData.fresa;

    }
}
