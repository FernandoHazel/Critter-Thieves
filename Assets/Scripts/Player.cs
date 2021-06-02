using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //TEMPORAL

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] UserInterface ui;
    Items item;
    Request request;

    public CharacterController controller;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    float savedSpeed, initialSpeed, initialJumpForce, speedPenalization, jumpForcePenalization, canClimbCounter = 0.5f, invencibilityTime = 0;

    private float verticalVelocity;
    private float gravity = 9.81f;

    public bool climb, vent, slow, saveItemPos, catPath;
    public Vector3 posInicial;
    [SerializeField] Transform posMarcel;
    GameObject cheeseSpawn;
    [SerializeField] GameObject foodDropper;

    public Text contQueso, contFresa, contNuez;

    public GameObject final;

    bool Boton, glueTraped;
    private Vector3 Front;
    int count, invCount = 0, MaxHp = 3;

    [SerializeField] SpriteRenderer[] sprites;

    public Transform BossCam;

    //We load the position saved only if any
    private void Awake() {
        invCount = PlayerPrefs.GetInt("invCount");
        if (invCount != 0)
        {
            //Debug.Log("intento cargar lista");
            //Debug.Log("invCount: " + invCount);
            string idABuscar;
            GameObject objeto;
            for (int i = 0; i < invCount; i++)
            {
                idABuscar = PlayerPrefs.GetString(i.ToString()); //este id tiene que coincidir con el nombre del objeto en la jerarquía
                objeto = GameObject.Find(idABuscar); //encontramos el objeto cuyo nombre coincide con el ID
                playerData.inventory.Add(objeto); //añadimos el objeto a la lista
                //Debug.Log("cargado objeto: " + objeto.name);
            }
            cheeseSpawn = playerData.inventory[playerData.inventory.Count - 1];
        }
    }
    void Start()
    {
        if (posInicial.x != 0)
        {
            transform.position = posInicial;
        }
        else
        {
            posInicial = transform.position;
        }
        //transform.position = playerData.posSaved;
        //these variables make the slow down work the same without
        //having to go back to the code every time we modify the speed
        //and the jump force
        initialSpeed = speed;
        initialJumpForce = jumpForce;
        speedPenalization = speed * .075f;
        jumpForcePenalization = jumpForce * .075f;

        
        controller = GetComponent<CharacterController>();
        Front = new Vector3(0, 0, -.3f);
        ui.UpdateHearts(playerData.Hp);
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
        if (Input.GetAxis("Horizontal") > 0)
        {
            foodDropper.transform.localPosition = new Vector3(1, 0, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            foodDropper.transform.localPosition = new Vector3(-1, 0, 0);
        }
    }

    void GetHurt() //Loose a Life
    {
        if (GameManager.pause) {
            return;
        }

        if (invencibilityTime > 0)
            return;
        playerData.Hp--;

        ui.UpdateHearts(playerData.Hp);

        invencibilityTime = 2;

        if (playerData.Hp <= 0)
        {
            Die();
        }
    }

    public void Die() //Reset Everything
    {
        for (int i = 0; i < playerData.inventory.Count; i++)
        {
            cheeseSpawn.transform.position = foodDropper.transform.position;

            Items tipo = cheeseSpawn.GetComponent<Items>();
            if (tipo.Tipo == "Fresa")
            {
                cheeseSpawn.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                cheeseSpawn.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                cheeseSpawn.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
                //cheeseSpawn.GetComponent<Items>().saveItemPos(cheeseSpawn.GetComponent<Items>().ID, transform.localPosition);
                playerData.fresa--;
                cheeseSpawn.GetComponent<Items>().grabbed = false;
                //Debug.Log("Fresa: " + cheeseSpawn.GetComponent<Items>().grabbed);
            }
            else if (tipo.Tipo == "Nuez")
            {
                cheeseSpawn.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                //cheeseSpawn.GetComponent<Items>().saveItemPos(cheeseSpawn.GetComponent<Items>().ID, transform.localPosition);
                playerData.nuez--;
                cheeseSpawn.GetComponent<Items>().grabbed = false;
                //Debug.Log("Nuez: " + playerData.nuez);
            }
            else if (tipo.Tipo == "Queso")
            {
                cheeseSpawn.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                //cheeseSpawn.GetComponent<Items>().saveItemPos(cheeseSpawn.GetComponent<Items>().ID, transform.localPosition);
                playerData.queso--;
                cheeseSpawn.GetComponent<Items>().grabbed = false;
                //Debug.Log("Queso: " + cheeseSpawn.GetComponent<Items>().grabbed);
            }

            playerData.inventory.Remove(cheeseSpawn);
            if (playerData.inventory.Count != 0)
            {
                cheeseSpawn = playerData.inventory[playerData.inventory.Count - 1];
                //cheeseSpawn = playerData.mochila[cheeseSpawn.GetComponent<Items>().ID];
            }
        }
        playerData.inventory.Clear();
        //Debug.Log("died");
        speed = initialSpeed;
        jumpForce = initialJumpForce;
        playerData.queso = 0;
        playerData.fresa = 0;
        playerData.nuez = 0;
        playerData.Score = 0;
        controller.enabled = false;
        transform.position = posInicial;
        controller.enabled = true;

        playerData.Hp = MaxHp;
        ui.UpdateHearts(MaxHp);
        //Debug.Log("Player 1: " + playerData.Score);

        ui.UpdateCheese(playerData.Score);
    }

    public void CamRun()
    {
        //Debug.Log(transform.position.x - BossCam.position.x);

        if (transform.position.x - BossCam.position.x <= -14)
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
        if (!other.GetComponent<Items>().grabbed && Input.GetKey(KeyCode.F) && playerData.Score < 4)
        {
            playerData.inventory.Add(other);
            //cheeseSpawn = playerData.inventory[playerData.inventory.Count - 1];
            //cheeseSpawn = other;

            Items tipo = other.GetComponent<Items>();
            if (tipo.Tipo == "Fresa")
            {
                playerData.fresa++;
                other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
                other.GetComponent<Items>().grabbed = true;
            }
            if (tipo.Tipo == "Nuez")
            {
                other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                playerData.nuez++;
                other.GetComponent<Items>().grabbed = true;
            }
            if (tipo.Tipo == "Queso")
            {
                playerData.queso++;
                other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                other.GetComponent<Items>().grabbed = true;
            }
            Boton = false;
        }
    }

    void dropFood() //Soltar comida
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerData.Score > 0)
            {
                cheeseSpawn.transform.position = foodDropper.transform.position;

                Items tipo = cheeseSpawn.GetComponent<Items>();
                if (tipo.Tipo == "Fresa")
                {
                    cheeseSpawn.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    cheeseSpawn.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                    cheeseSpawn.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
                    playerData.fresa--;
                    cheeseSpawn.GetComponent<Items>().grabbed = false;
                }
                else if (tipo.Tipo == "Nuez")
                {
                    cheeseSpawn.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    playerData.nuez--;
                    cheeseSpawn.GetComponent<Items>().grabbed = false;
                }
                else if (tipo.Tipo == "Queso")
                {
                    cheeseSpawn.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    playerData.queso--;
                    cheeseSpawn.GetComponent<Items>().grabbed = false;
                }

                playerData.inventory.Remove(cheeseSpawn);
                if (playerData.inventory.Count != 0)
                {
                    cheeseSpawn = playerData.inventory[playerData.inventory.Count - 1];
                }
    
            }
            else
            {
                Debug.Log("No puedes soltar nada porque nada tienes");
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

        /*if (other.gameObject.tag == "Nephew")
        {
            Nephew();
            Boton = true;
        }*/
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
            glueTraped = true;
            savedSpeed = speed;
            speed = 0.5f;
            //Debug.Log("In" + speed);
            slow = true;
        }
        
        if (other.gameObject.tag == "Food")
        {
            //object = other.gameObject;
            //canGrab = true;
            //F.SetActive(true);
        } 
        if (other.gameObject.tag == "Checkpoint")
        {
            saveItemPos = true;
            posInicial = other.transform.position;
            playerData.save();
            //guardamos los objetos de la lista
            for (int i = 0; i < playerData.inventory.Count; i++) //con esto recorremosla lista y guardamos índices y ID de los objetos
            {
                PlayerPrefs.SetString(i.ToString(), playerData.inventory[i].GetComponent<Items>().ID);
            }
            invCount = playerData.inventory.Count;
            PlayerPrefs.SetInt("invCount", invCount);
            
            //Debug.Log("initial position updated");
        }
        if (other.gameObject.tag == "Nephew")
        {
            vent = true;
            Boton = true;
        }
        if (other.gameObject.tag == "CatPath")
        {
            Debug.Log("entrando gato");
            catPath = true;
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
            //canGrab = false;
            Boton = false;
            //F.SetActive(false);
        }

        if (other.gameObject.tag == "GlueTrap")
        {
            glueTraped = false;
            speed = savedSpeed;
            //Debug.Log("Out" + speed);
            slow = false;
        }

        if (other.gameObject.tag == "Nephew")
        {
            Boton = false;
            posInicial = transform.position;
            vent = false;
        }
        if (other.gameObject.tag == "Checkpoint")
        {
            saveItemPos = false;
        }
        if (other.gameObject.tag == "CatPath")
        {
            catPath = false;
        }
        if (other.gameObject.tag == "Finish")
        {
            final.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void regulator()
    {
        if (playerData.Hp > 3)
        {
            playerData.Hp = 3;
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
        // aquí nos evitamos llamar estas lineas en los métodos de arriba y es más legible el código
        if (!glueTraped)
        {
            speed = (initialSpeed - speedPenalization * playerData.Score);
        }
        jumpForce = (initialJumpForce - jumpForcePenalization * playerData.Score);
        ui.UpdateHearts(playerData.Hp);
        ui.UpdateCheese(playerData.Score);
        if (playerData.inventory.Count != 0)
        {
            cheeseSpawn = playerData.inventory[playerData.inventory.Count - 1];
        }
    }
    private void Update()
    {
        if (GameManager.pause) //Activar la pausa
        {
            return;
        }
        //GrabFood();
        Blink();
        Movement();
        dropFood();
        regulator();
        CamRun();

        contNuez.text = "" + playerData.nuez;
        contQueso.text = "" + playerData.queso;
        contFresa.text = "" + playerData.fresa;

        //Debug.Log(transform.position.x - BossCam.position.x);
    }
}
