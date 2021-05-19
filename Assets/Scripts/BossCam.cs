using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{

    public GameObject tracker;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;  //this is the time of delay of the camera movement
    private Vector2 velocity;



    public float speed = 0f;  //velocidad de cubo

    public float margenError = 0f;

    int estados = 0;

    public int modos = 0;

    bool irAdelante = true;

    bool loop = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Rutas()
    {
        transform.position = Vector3.Lerp(transform.position, , speed * Time.deltaTime);

    }
    // Update is called once per frame
    void Update()
    {;
        float posY = Mathf.SmoothDamp(transform.position.y, tracker.transform.position.y + 2.5f, ref velocity.y, smoothTime);

        //We move the camera to that position
        //We use the "Clampt" function to limit the camera movement between a minimum and a maximum position in X and Y axis
        transform.position = new Vector3(Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);
    }
}
