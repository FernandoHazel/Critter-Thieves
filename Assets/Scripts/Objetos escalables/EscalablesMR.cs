using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EscalablesMR : MonoBehaviour
{
    //Material material;
    Color color;
    float colorBlue;
    float num = 0;
    [SerializeField] Material material;
    float effectTime = 0.4f; // duración del efecto
    //al mover el effectIntensity hay que modificar el effectTime
    float effectIntensity = 0.7f; //intensidad del amarillo (0 lo más amarillo, 1 lo menos amarillo)
    bool subir;
    void Start()
    {
        //colorBlue = 1;
        //color = Color.white;
        //material = GetComponent<Material>();
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (subir)
        {
            colorBlue = colorBlue + effectTime * Time.deltaTime;
        }
        else
        {
            colorBlue = colorBlue - effectTime * Time.deltaTime;
        }
        
        if (colorBlue >= 1)
        {
            subir = false;
        }
        else if (colorBlue <= effectIntensity)
        {
            subir = true;
        }

        //esto setea el color del tint
        color = new Color(1,colorBlue,1,1);
        material.SetColor("_Color", color);
    }
}
