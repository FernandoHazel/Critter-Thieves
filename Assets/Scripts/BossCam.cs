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
    public Transform Player;

    Vector3 posInicial;



    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
    }


    void Rutas()
    {
        float posY = Mathf.SmoothDamp(transform.position.y, tracker.transform.position.y + 2.5f, ref velocity.y, smoothTime);


        if (transform.position.x - Player.position.x >= 15)
        {

            transform.position = posInicial;
        }

        if (transform.position.x - End.position.x != 0)
        {
            transform.position = new Vector3(transform.position.x + (Time.deltaTime * 6.3f), Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);
        }



            
    }
    // Update is called once per frame
    void Update()
    {

            Rutas();

        //Debug.Log(transform.position.x - Player.position.x);


    }
}
