using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string Tipo;
    float rotar = 30;
    //float speed = .03f;
    //float delta = .2f;

    Vector3 posInicial;

    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;

    }

    void Rotacion()
    {

        //transform.rotation(0, _rotationSpeed* Time.deltaTime, 0);
        transform.Rotate(0, rotar * Time.deltaTime, 0);
    }


void Update()
    {
      
        Rotacion();
        // Rotation on y axis
    }
}
