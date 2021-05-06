using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool slow = false;
    [SerializeField] UserInterface ui;
    public CharacterController controller;
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

    public bool climb = false;
    //bool climbJump = false;

    private Vector3 Front;

    public int Score = 0;
    int queso = 0;
    int fresa = 0;
    int nuez = 0;
    List<GameObject> food = new List<GameObject>();
    [SerializeField] SpriteRenderer[] sprites;
    float initialSpeed;
    float initialJumpForce;
    float speedPenalization;
    float jumpForcePenalization;

    void Start()
    {
        //these variables make the slow down work the same without
        //having to go back to the code every time we modify the speed
        //and the jump force
        initialSpeed = speed;
        initialJumpForce = jumpForce;
        speedPenalization = speed * .1666666f;
        jumpForcePenalization = jumpForce * .1666666f;

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
            jumpForce=3;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        //This is the lateral movement
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), verticalVelocity, 0);
        controller.Move(moveVector * speed * Time.deltaTime);
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
        

        for (int i = 0; i < food.Count; i++)
        {
            food[i].SetActive(true);
        }
        speed = initialSpeed;
        Score = 0;
        food.Clear();
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;
        Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        Debug.Log("Player 1: " + Score);
        queso = 0;
        fresa = 0;
        nuez = 0;
        jumpForce = initialJumpForce;
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

            jumpForce = (jumpForce - jumpForcePenalization);
            speed = (speed - speedPenalization);

            Cheese.gameObject.SetActive(false);
            Debug.Log("Food: " + Score);
            food.Add(Cheese);
            
            ui.UpdateCheese(queso);
            Boton = false;

        }
    }

    public void GrabBerry(GameObject Berry) //Mecanismo para el queso
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Score++;
            fresa++;

            jumpForce = (jumpForce - jumpForcePenalization);
            speed = (speed - speedPenalization);

            Berry.gameObject.SetActive(false);
            Debug.Log("Food: " + Score);
            food.Add(Berry);

            ui.UpdateCheese(fresa);
            Boton = false;

        }
    }


    public void GrabNut(GameObject Nut) //Mecanismo para el queso
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Score++;
            nuez++;

            jumpForce = (jumpForce - jumpForcePenalization);
            speed = (speed - speedPenalization);

            Nut.gameObject.SetActive(false);
            Debug.Log("Food: " + Score);
            food.Add(Nut);

            ui.UpdateCheese(nuez);
            Boton = false;

        }
    }

    void dropFood() //Soltar comida
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Score > 0)
            {
                queso--;
                Score--;

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

    void Nephew()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Score > 0)
            {
                Hp = Hp + (Score);
                queso = 0;
                nuez = 0;
                fresa = 0;
                Score = 0;
                jumpForce = initialJumpForce;
                speed = initialSpeed;

                ui.UpdateCheese(queso);
                ui.UpdateHearts(Hp);
                Debug.Log("Food: " + Score);
            }
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

        if (other.gameObject.tag == "Berry")
        {
            GrabBerry(other.gameObject);
            Boton = true;

        }

        if (other.gameObject.tag == "Nuts")
        {
            GrabNut (other.gameObject);
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
            Debug.Log("In" + speed);
            slow = true;
        }

        if (other.gameObject.tag == "Cheese")
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
        if (other.gameObject.tag == "Cheese")
        {
            GrabCheese(other.gameObject);
            Boton = false;
            //F.SetActive(false);
        }

        if (other.gameObject.tag == "GlueTrap")
        {
            speed = savedSpeed;
            Debug.Log("Out" + speed);
            slow = false;
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
