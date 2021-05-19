using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{

    public GameObject tracker;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;  //this is the time of delay of the camera movement
    private Vector2 velocity;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Rutas()
    {
        float posY = Mathf.SmoothDamp(transform.position.y, tracker.transform.position.y + 2.5f, ref velocity.y, smoothTime);
        transform.position = new Vector3(transform.position.x + (Time.deltaTime*5), Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);

    }
    // Update is called once per frame
    void Update()
    {
        Rutas();

        //float posY = Mathf.SmoothDamp(transform.position.y, tracker.transform.position.y + 2.5f, ref velocity.y, smoothTime);

        //We move the camera to that position
        //We use the "Clampt" function to limit the camera movement between a minimum and a maximum position in X and Y axis
        //transform.position = new Vector3( 1 * Time.deltaTime, Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);
    }
}
