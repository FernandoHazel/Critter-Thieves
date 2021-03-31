using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //We create a pause, we make it static so that it is the same in all scripts
    public static bool pause = false;

    // Start is called before the first frame update
    //void Start(){}


    // Update is called once per frame
    void Update()
    {
        //every time we press the "P" the boolean "pause" will change
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            Debug.Log("pause is " + pause);
        }
    }
    
}
