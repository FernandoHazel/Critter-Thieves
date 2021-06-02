using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Player playerScript;
    public string Tipo;
    
    public string ID;
    float rotar = 30;
    public bool grabbed;

    private void Awake() {
        //ID = transform.localPosition.ToString();
        //playerData.grabbableObjs.Add(ID, gameObject); //todos los objetos se registran en el diccionario con su ID y gameObject
    }
    void Start()
    {
        //load the item position
        float posX = PlayerPrefs.GetFloat(ID + "x");
        if (posX != 0)
        {
            Vector3 savedPos = new Vector3 (posX, PlayerPrefs.GetFloat(ID + "y"), 0);
            transform.position = savedPos;
        }
        //load the item bool grabbed
        grabbed = PlayerPrefs.GetInt(ID + " grabbed") > 0 ? true : false;
        //Debug.Log("loaded "+ Tipo + grabbed);
    }

    void Rotacion()
    {
        transform.Rotate(0, rotar * Time.deltaTime, 0);
    }


    public void saveItemPos()//we save the item position and the boolean of on and off
    {
        float positionX = transform.localPosition.x;
        float positionY = transform.localPosition.y;
        PlayerPrefs.SetFloat(ID + "x", positionX);
        PlayerPrefs.SetFloat(ID + "y", positionY);
        //Debug.Log("saved "+ Tipo + ID);
        PlayerPrefs.SetInt(ID + " grabbed" , grabbed? 1 : 0);
        //Debug.Log("saved "+ Tipo + grabbed);
    }
    void Update()
    {
        if (playerScript.saveItemPos)
        {
            saveItemPos();
        }
        Rotacion(); // Rotation on y axis
        if (grabbed)
        {
            if (Tipo == "Fresa")
            {
                gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
