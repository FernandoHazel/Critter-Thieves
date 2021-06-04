using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    float speed = 10;
    void Update()
    {
        TrackerMovement();
    }

    void TrackerMovement1()
    {
        /*if (Input.GetAxis("Horizontal") == 1)
        {
            Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.Translate(moveVector * speed * Time.deltaTime);
            if (transform.localPosition.x >= 4)
            {
                float variable = transform.localPosition.x;
                variable = 4;
            }
        }
        else if (Input.GetAxis("Horizontal") == -1)
        {
            Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.Translate(moveVector * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") == 1)
        {
            Vector3 moveVector = new Vector3(0, Input.GetAxis("Vertical"), 0);
            transform.Translate(moveVector * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") == -1)
        {
            Vector3 moveVector = new Vector3(0, Input.GetAxis("Vertical"), 0);
            transform.Translate(moveVector * speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }*/
    }
    void TrackerMovement()
    {
       if (Input.GetAxis("Horizontal") == 1)
        {
            transform.localPosition = new Vector3(8, 0, 0);
        }
        else if (Input.GetAxis("Horizontal") == -1)
        {
            transform.localPosition = new Vector3(-8, 0, 0);
        }
        /*else if (Input.GetAxis("Vertical") == 1)
        {
            transform.localPosition = new Vector3(0, 4, 0);
        }
        else if (Input.GetAxis("Vertical") == -1)
        {
            transform.localPosition = new Vector3(0, -5, 0);
        }*/
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
