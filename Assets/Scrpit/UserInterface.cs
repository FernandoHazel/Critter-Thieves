using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Image[] Hearts;
    public Image ButtonLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateHearts(int numberOfHearts)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
           if (i <= numberOfHearts)
            {
                Hearts[i].enabled = i <= numberOfHearts;
            }
        }
    }

    public void UpdateButton()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
