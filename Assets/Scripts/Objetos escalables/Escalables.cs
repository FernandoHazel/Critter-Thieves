using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Escalables : MonoBehaviour
{
    Material material;
    Color color;
    float colorBlue;
    float num = 0;
    void Start()
    {
        colorBlue = 1;
        color = Color.white;
        material = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if(colorBlue>=0)
        {
            colorBlue = colorBlue + 0.1f;
        }
        if(colorBlue>1)
        {
            colorBlue = colorBlue - 0.1f;
        }
        color = new Color(1,1,colorBlue,1);
        material.SetColor("_Color", color);
    }
}
