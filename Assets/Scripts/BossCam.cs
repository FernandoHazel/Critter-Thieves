using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{

    public GameObject tracker;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;  //this is the time of delay of the camera movement
    private Vector2 velocity;

    public Transform End;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Rutas()
    {
        float posX = transform.position.x;
        float posY = Mathf.SmoothDamp(transform.position.y, tracker.transform.position.y + 2.5f, ref velocity.y, smoothTime);
        transform.position = new Vector3(transform.position.x + (Time.deltaTime*6.3f), Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);

        if (transform.position.x == End.position.x)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); ;
        }

    }
    // Update is called once per frame
    void Update()
    {
        Rutas();

    }
}
