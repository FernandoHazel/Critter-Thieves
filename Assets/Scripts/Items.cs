using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public string Tipo;

    bool grabbed;
    float rotar = 30;

    Vector3 posInicial;
    void Start()
    {
        posInicial = transform.position;
    }

    void Rotacion()
    {
        transform.Rotate(0, rotar * Time.deltaTime, 0);
    }

    public void Grabed(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Food grabbed");
            grabbed = true;
            gameObject.SetActive(false);
            if (Tipo == "Fresa")
            {
                Debug.Log("Es Fresa");
                playerData.fresa++;
                playerData.Score++;
                Debug.Log("Fresas: " + playerData.fresa);
            }
            if (Tipo == "Nuez")
            {
                Debug.Log("Es Nuez");
                playerData.nuez++;
                playerData.Score++;
                Debug.Log("Nueces: " + playerData.nuez);
            }
            if (Tipo == "Queso")
            {
                Debug.Log("Es Queso");
                playerData.queso++;
                playerData.Score++;
                Debug.Log("Queso: " + playerData.queso);
            }
        }
        
    }
    public void dropped()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerData.Score > 0)
            {
                grabbed = false;
            }
        }
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
