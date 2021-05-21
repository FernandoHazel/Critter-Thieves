using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Playerdata/saveAndLoad", order = 0)]
public class PlayerData : ScriptableObject
{
    public grabbableObjects[] grabbableObjs;
    public List<GameObject> inventory = new List<GameObject>(); //This is the inventary of the food
    public Dictionary<string, GameObject> mochila = new Dictionary<string, GameObject>();//This saves the positions of the food, do not appears in the inspector
    
    public int Score = 0;
    public int queso = 0;
    public int fresa = 0;
    public int nuez = 0;
    public Vector3 posSaved = new Vector3(-113, 2, 0); //position of the player
    float posX;
    float posY;

    
    //Save the position of the player
    //Save the food counters
    public void save()
    {
        float posX = posSaved.x;
        float posY = posSaved.y;
        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        PlayerPrefs.SetInt("score", Score);
        PlayerPrefs.SetInt("queso", queso);
        PlayerPrefs.SetInt("fresa", fresa);
        PlayerPrefs.SetInt("nuez", nuez);
        Debug.Log("Game saved");
    }
    
    
    public void load()
    {
        PlayerPrefs.GetFloat("posX", posX);
        PlayerPrefs.GetFloat("posY", posY);
        PlayerPrefs.GetInt("score", Score);
        PlayerPrefs.GetInt("queso", queso);
        PlayerPrefs.GetInt("fresa", fresa);
        PlayerPrefs.GetInt("nuez", nuez);
        posSaved.x = posX;
        posSaved.y = posY;
        Debug.Log("Game loaded");
    }
    [Serializable]
    public class grabbableObjects
    {
        public GameObject food;
        public bool grabbed;
    }
}
