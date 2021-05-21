using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public string Tipo;
    
    public string ID;
    float rotar = 30;


    void Start()
    {
        ID = transform.localPosition.ToString();
        float posX = PlayerPrefs.GetFloat(ID + "x");
        if (posX != 0)
        {
            Vector3 savedPos = new Vector3 (posX, PlayerPrefs.GetFloat(ID + "y"), 0);
            transform.position = savedPos;
        }
        //if (Tipo == "Queso")
        //{
            //print (Tipo + " posX " + posX);
        //}
        
    }

    void Rotacion()
    {
        transform.Rotate(0, rotar * Time.deltaTime, 0);
    }

    public void Grabed(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("Food grabbed");
            gameObject.SetActive(false);
            if (Tipo == "Fresa")
            {
                //Debug.Log("Es Fresa");
                playerData.fresa++;
                playerData.Score++;
                //Debug.Log("Fresas: " + playerData.fresa);
            }
            if (Tipo == "Nuez")
            {
                //Debug.Log("Es Nuez");
                playerData.nuez++;
                playerData.Score++;
                //Debug.Log("Nueces: " + playerData.nuez);
            }
            if (Tipo == "Queso")
            {
                //Debug.Log("Es Queso");
                playerData.queso++;
                playerData.Score++;
                //Debug.Log("Queso: " + playerData.queso);
            }
        }
        
    }
    public void dropped()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //
        }
    }
    public void saveItemPos(string id, Vector3 position)// saves the position of a dropped object to spawn it in the next game
    {
        float positionX = position.x;
        float positionY = position.y;
        PlayerPrefs.SetFloat(id + "x", positionX);
        PlayerPrefs.SetFloat(id + "y", positionY);
        //if (Tipo == "Queso")
        //{
            //Debug.Log(Tipo + " positioX " + positionX);
        //}
    }
    public void saveItemID(string id)//this saves the ID of a grabbed object
    {
        PlayerPrefs.SetString(id, id);
    }
    public void eraseItemID(string id)//this erases the ID of a dropped object
    {
        PlayerPrefs.DeleteKey(id);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Grabed(other.gameObject);
        }
    }
    void Update()
    {
        dropped();  
        Rotacion(); // Rotation on y axis
    }
}
