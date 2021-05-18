using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    Rigidbody m_Box;
    Vector3 direccion;

    // Start is called before the first frame update
    void Start()
    {
        m_Box = GetComponent<Rigidbody>();
        direccion = new Vector3 (Input.GetAxis("Horizontal"),0,0);
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_Box.AddForce(Input.GetAxis("Horizontal")*.3f , 0, 0, ForceMode.Impulse);
            Debug.Log("me tocaste");
        }
    }
    // Update is called once per frame
    void Update()
    { 
    }
}
