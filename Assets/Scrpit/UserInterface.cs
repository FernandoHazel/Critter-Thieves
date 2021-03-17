using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Image Hearts;
    public Image ButtonLight;

    // Start is called before the first frame update


    public void UpdateHearts(float hp)
    {
        Hearts.fillAmount = hp;

    }

    public void UpdateButton()
    {

    }

}
