using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Image[] hearts;
    public Image[] chesse;

    public void UpdateHearts(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hp;
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
