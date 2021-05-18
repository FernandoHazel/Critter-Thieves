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

    void Push()
    {
        m_Box.AddForce(direccion, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            //
            Debug.Log("me tocaste");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Push();   
    }
}
