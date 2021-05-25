using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Dictionary<string, GameObject> grabbableObjs = new Dictionary<string, GameObject>(); 
    public List<GameObject> inventory = new List<GameObject>(); //This is the inventary of the food
    [SerializeField] Player playerScript;
    public int Score = 0;
    public int queso = 0;
    public int fresa = 0;
    public int nuez = 0;
    public int Hp = 3; //Health
    public Vector3 posSaved = Vector3.zero; //position of the player
    float playerPosX; //
    float playerPosY;

    private void Awake() {
        load();
        if (Hp <= 0)
        {
            Hp = 3;
        }
    }
    private void Start() {
        //Debug.Log(grabbableObjs);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G))
        {
            save();
        }
    }

    //Save the position of the player
    //Save the food counters
    public void save()
    {
        posSaved = playerScript.transform.position;
        float playerPosX = posSaved.x;
        float playerPosY = posSaved.y;
        PlayerPrefs.SetFloat("playerPosX", playerPosX);
        PlayerPrefs.SetFloat("playerPosY", playerPosY);
        PlayerPrefs.SetInt("score", Score);
        PlayerPrefs.SetInt("queso", queso);
        PlayerPrefs.SetInt("fresa", fresa);
        PlayerPrefs.SetInt("nuez", nuez);
        PlayerPrefs.SetInt("hp", Hp);
        Debug.Log("Game saved");
    }
    
    public void load()
    {
        float playerPosX = PlayerPrefs.GetFloat("playerPosX");
        if (playerPosX != 0)
        {
            posSaved = new Vector3 (playerPosX, PlayerPrefs.GetFloat("playerPosY"), 0);
            playerScript.transform.position = posSaved;
        }
        
        Score = PlayerPrefs.GetInt("score");
        queso = PlayerPrefs.GetInt("queso");
        fresa = PlayerPrefs.GetInt("fresa");
        nuez = PlayerPrefs.GetInt("nuez");
        Hp = PlayerPrefs.GetInt("hp");
        //Debug.Log(posSaved.x);
        //Debug.Log(playerPosX);
        //Debug.Log("Game loaded");
    }
}
