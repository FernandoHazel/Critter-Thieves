using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Request : MonoBehaviour
{
    Player player;
    public bool misiones; //este bool nos señala si las quest fueron completadas o no
    public int rqFresa;
    public int rqNuez;
    public int rqQueso;

    void Start()
    {

    }

    
    void Request1(GameObject player)
    {
        //Player variable = player.GetComponent<Player>();
        //rqFresa = variable.fresa;
        /*
            rqFresa = 1;
            rqNuez = 2;
            rqQueso = 1;

            if (player.queso == rqQueso) 
            {
                Debug.Log("Request 1 COMPLETADO");
            }
        */
    }

    void NextLevel()
    {
        if(misiones == true)
        {
            SceneManager.LoadScene(2);
        }
    }


    
    void Update()
    {
        NextLevel();

        //Request1(player.gameObject);
    }
}
