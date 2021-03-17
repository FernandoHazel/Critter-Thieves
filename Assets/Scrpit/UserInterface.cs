using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Image[] Hearts;
    public Image ButtonLight;
    public Image[] Stillson;
    public Image[] Cheese;

    // Start is called before the first frame update


    public void UpdateHearts(int hp)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].enabled = i < hp;
        }

    }

    public void UpdateButton(bool boton)
    {
        ButtonLight.enabled = boton;
    }

    public void UpdateStillson(int keys)
    {

        for (int i=0; i < Stillson.Length; i++)
        {
            Stillson[i].enabled = i < keys;
        }

    }

    public void UpdateCheese(int cheese)
    {
        for (int i = 0; i < Cheese.Length; i++)
        {
            Cheese[i].enabled = i < cheese;
        }
    }

}
