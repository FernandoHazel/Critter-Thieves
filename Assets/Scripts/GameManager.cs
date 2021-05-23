﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //We create a pause, we make it static so that it is the same in all scripts
    public static bool pause = false;

    [SerializeField] PlayerData playerData;

    //this variables holds the different canvas objects of the pause and the running game
    public GameObject inGame;
    public GameObject inPause;

    //This void activates and desactivates the menu if in pause or in game
    void ManageMenu(){
        if (pause){
            inGame.SetActive(false);
            inPause.SetActive(true);
            //pauseM.Holder.SetActive(true);
        }
        if (!pause){
            inGame.SetActive(true);
            inPause.SetActive(false);
            //howToPlay.SetActive(false);
        }
    }
    private void Start() {
        //
    }
    void Update()
    {
        //every time we press the "P" the boolean "pause" will change
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            //Debug.Log("pause is " + pause);
        }
        ManageMenu();
    }
}
