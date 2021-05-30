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
    public int comida = 3;
    int request = 0;

    void Start()
    {

    }


    void Request1()
    {
        //Player variable = player.GetComponent<Player>();
        switch (request)
        {
            case 0:
                if (rqFresa == comida)
                {
                    request++;

                }

                break;

            case 1:
                if (rqNuez == comida)
                {
                    request++;

                }

                break;

            case 2:
                if (rqQueso == comida)
                {
                    misiones = true;

                }
                break;
        }

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
