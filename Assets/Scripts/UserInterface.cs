using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Image[] hearts;
    public Image buttonLigth;
    public Image[] stillson;
    public Image[] chesse;

    // Start is called before the first frame update


    public void UpdateHearts(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hp;
        }

    }

    public void UpdateButton(bool boton)
    {
        buttonLigth.enabled = boton;
    }

    public void UpdateStillson(int keys)
    {

        for (int i=0; i < stillson.Length; i++)
        {
            stillson[i].enabled = i < keys;
        }

    }

    public void UpdateCheese(int cheese)
    {
        for (int i = 0; i < chesse.Length; i++)
        {
            chesse[i].enabled = i < cheese;
        }
    }

}
