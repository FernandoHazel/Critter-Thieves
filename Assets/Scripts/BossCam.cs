using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Rutas()
    {
        transform.position = Vector3.Lerp(transform.position, waypoints[estados].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[estados].position) < margenError)
        {
            estados++;

            if (estados >= waypoints.Length)
            {
                estados--;
                modos = -1;
                //Debug.Log("Detente");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
