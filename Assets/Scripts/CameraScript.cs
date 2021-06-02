using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Player playerScript;
    public GameObject tracker;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;  //this is the time of delay of the camera movement
    private Vector2 velocity;

    // Update is called once per frame
    void Update()
    {
        /*if (!playerScript.controller.isGrounded && !playerScript.climb)
        {
            smoothTime = .1f;
        }
        else{
            smoothTime = 0.5f;
        }*/
        //float posX = tracker.transform.position.x;
        //float posY = tracker.transform.position.y;
        //We save the position of the tracker in new variables
        //The "smoothDamp" function creates a transition delay between to points and needs a reference of velocity and time
        float posX = Mathf.SmoothDamp(transform.position.x, tracker.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, tracker.transform.position.y + 2.5f, ref velocity.y, smoothTime);

        //We move the camera to that position
        //We use the "Clampt" function to limit the camera movement between a minimum and a maximum position in X and Y axis
        transform.position = new Vector3 (
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }
}
