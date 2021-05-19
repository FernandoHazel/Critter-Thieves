using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Playerdata/saveAndLoad", order = 0)]
public class PlayerData : ScriptableObject
{
    public Vector3 posSaved = new Vector3(-113, 2, 0);
    float posX;
    float posY;
    private void Start() 
    {
        load();
    }

    public void save()
    {
        float posX = posSaved.x;
        float posY = posSaved.y;
        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        Debug.Log("position saved");
    }
    public void load()
    {
        PlayerPrefs.GetFloat("posX", posX);
        PlayerPrefs.GetFloat("posY", posY);
        posSaved.x = posX;
        posSaved.y = posY;
    }
}
