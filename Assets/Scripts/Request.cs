using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Request : MonoBehaviour
{
    [SerializeField] Player playerScript;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject catPath, requestObject, PalomitasObject, palomitasPause, RequestsPause;
    public bool misiones; //este bool nos señala si las quest fueron completadas o no
    public int rqFresa, rqNuez, rqQueso, numPalomita = 0;
    int request = 0;

    void Start()
    {
        if(PlayerPrefs.GetInt("mision") != 0)
        {
            request = PlayerPrefs.GetInt("mision");
            
        }
        rqFresa = PlayerPrefs.GetInt("FresasEnt");
        rqNuez = PlayerPrefs.GetInt("NuecesEnt");
        rqQueso = PlayerPrefs.GetInt("QuesosEnt");
    }


    void RequestGame()
    {
        switch (request)
        {
            case 0:
                //Debug.Log("Entrega 4 fresas");
                if (playerScript.vent)
                {
                    //Debug.Log("intento entregar");
                    for (int i = 0; i<playerData.inventory.Count; i++)
                    {
                        //Debug.Log("itero");
                        if (playerData.inventory[i].GetComponent<Items>().Tipo == "Fresa")
                        {
                            //Debug.Log("encontré una fresa");
                            playerData.fresa--;
                            playerData.inventory.Remove(playerData.inventory[i]);
                            playerData.Hp++;
                            rqFresa++;
                            PlayerPrefs.SetInt("FresasEnt", rqFresa);
                            PalomitasObject.transform.GetChild(numPalomita).gameObject.SetActive(true);
                            palomitasPause.transform.GetChild(numPalomita).gameObject.SetActive(true);//
                            numPalomita++;
                        }
                    }
                    if (numPalomita >= 4) //Desactivamos las palomitas al terminar el request
                    {
                        for (int i = 0; i<PalomitasObject.transform.childCount; i++)
                        {
                            PalomitasObject.transform.GetChild(i).gameObject.SetActive(false);
                            palomitasPause.transform.GetChild(i).gameObject.SetActive(false);//
                        }
                        numPalomita = 0;
                    }
                }
                break;

            case 1:
                //Debug.Log("Entrega 4 nueces");
                if (playerScript.vent)
                {
                    for (int i = 0; i<playerData.inventory.Count; i++)
                    {
                        if (playerData.inventory[i].GetComponent<Items>().Tipo == "Nuez")
                        {
                            playerData.nuez--;
                            playerData.inventory.Remove(playerData.inventory[i]);
                            playerData.Hp++;
                            rqNuez++;
                            PlayerPrefs.SetInt("NuecesEnt", rqNuez);
                            PalomitasObject.transform.GetChild(numPalomita).gameObject.SetActive(true);
                            palomitasPause.transform.GetChild(numPalomita).gameObject.SetActive(true);
                            numPalomita++;
                        }
                    }
                    if (numPalomita >= 4) //Desactivamos las palomitas al terminar el request
                    {
                        for (int i = 0; i<PalomitasObject.transform.childCount; i++)
                        {
                            PalomitasObject.transform.GetChild(i).gameObject.SetActive(false);
                            palomitasPause.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        numPalomita = 0;
                    }
                }
                break;

            case 2:
                //Debug.Log("Entrega 4 quesos");
                if (playerScript.vent)
                {
                    for (int i = 0; i<playerData.inventory.Count; i++)
                    {
                        if (playerData.inventory[i].GetComponent<Items>().Tipo == "Queso")
                        {
                            playerData.queso--;
                            playerData.inventory.Remove(playerData.inventory[i]);
                            playerData.Hp++;
                            rqQueso++;
                            PlayerPrefs.SetInt("QuesosEnt", rqQueso);
                            PalomitasObject.transform.GetChild(numPalomita).gameObject.SetActive(true);
                            palomitasPause.transform.GetChild(numPalomita).gameObject.SetActive(true);
                            numPalomita++;
                        }
                    }
                }
                break;
        }

    }
    void NextMision()
    {
        if (rqFresa < 4)
        {
            //los request en la pausa
            RequestsPause.transform.GetChild(0).gameObject.SetActive(true);
            RequestsPause.transform.GetChild(1).gameObject.SetActive(false);
            RequestsPause.transform.GetChild(2).gameObject.SetActive(false);

            //los request en el juego
            requestObject.transform.GetChild(0).gameObject.SetActive(true);
            requestObject.transform.GetChild(1).gameObject.SetActive(false);
            requestObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (rqFresa >= 4)
        {
            request = 1;
            PlayerPrefs.SetInt("mision", request);

            RequestsPause.transform.GetChild(0).gameObject.SetActive(false);
            RequestsPause.transform.GetChild(1).gameObject.SetActive(true);
            RequestsPause.transform.GetChild(2).gameObject.SetActive(false);

            requestObject.transform.GetChild(0).gameObject.SetActive(false);
            requestObject.transform.GetChild(1).gameObject.SetActive(true);
            requestObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (rqNuez >= 4)
        {
            request = 2;
            PlayerPrefs.SetInt("mision", request);

            RequestsPause.transform.GetChild(0).gameObject.SetActive(false);
            RequestsPause.transform.GetChild(1).gameObject.SetActive(false);
            RequestsPause.transform.GetChild(2).gameObject.SetActive(true);

            requestObject.transform.GetChild(0).gameObject.SetActive(false);
            requestObject.transform.GetChild(1).gameObject.SetActive(false);
            requestObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (rqQueso == 4)
        {
            request = 3; // esto no está haciendo nada de momento
            PlayerPrefs.SetInt("mision", request);
            misiones = true;
            Debug.Log("NIVEL COMPLETADO");
            playerScript.final.SetActive(true);
            Time.timeScale = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                playerScript.final.SetActive(false);
                Time.timeScale = 1;
                rqQueso++;
            }
            
        }
        /*Debug.Log("Fresas entregadas: " + rqFresa);
        Debug.Log("Nueces entregadas: " + rqNuez);
        Debug.Log("Quesos entregadas: " + rqQueso);
        Debug.Log("misiones " + misiones);*/
    }
    void GoToCatLevel()
    {
        Debug.Log("al nivel del gato");
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Corridor");
    }
    
    void Update()
    {
        NextMision();
        RequestGame();
        if (playerScript.catPath && misiones)
        {
            GoToCatLevel();
        }
        else if (playerScript.catPath && !misiones)
        {
            //Sale mensaje de que aún falta recoger comida
        }
    }
}
