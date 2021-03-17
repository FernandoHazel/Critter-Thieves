using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Image Hearts;
    public Image ButtonLight;
    public Image[] Stillson;

    // Start is called before the first frame update


    public void UpdateHearts(float hp)
    {
        Hearts.fillAmount = hp;

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

}
