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
                    int contFresas;
                    //si estás en el collider de los sobrinos (booleano del player == true)
                    //Si presionas F
                    //for que itere según la longitud del inventario
                    /*for (int i = 0; i<PlayerData.inventory.Count; i++)
                    {
                        if (PlayerData.inventory[i].GetComponent<Items>.Tipo == "fresa")
                        {
                            PlayerData.inventory[i].Remove();
                            Player.Hp++;
                            contFresas++;
                        }
                    }
                    if (contFresas>=4)
                    {
                        request++;
                    }*/
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


    
    void Update()
    {
        //Request1();
    }
}
