using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] int downSpeed = 5;
    [SerializeField] float ligerez = 0.5f;
    bool onPush;
    Rigidbody mbox;
    Vector3 direccion;

    // Start is called before the first frame update
    void Start()
    {
        mbox = GetComponent<Rigidbody>();
        direccion = new Vector3 (Input.GetAxis("Horizontal"),0,0);
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onPush = true;
            mbox.AddForce(Input.GetAxis("Horizontal") * ligerez, 0, 0, ForceMode.VelocityChange);
            //Debug.Log("me tocaste");
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            onPush = false;
        }
    }
    private void FixedUpdate() {
        if (!onPush)
        {
            mbox.AddForce(0, -downSpeed, 0);
        }
    }
}
