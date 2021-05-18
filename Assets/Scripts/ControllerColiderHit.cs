using UnityEngine;
using System.Collections;

public class ControllerColiderHit : MonoBehaviour
{

    Vector3 direccion;

    private void Start()
    {
        direccion = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }
   
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        Debug.Log("me tocaste");

        if (Input.GetKey(KeyCode.F))
            {

                body.AddForce(direccion, ForceMode.Impulse);
            }
        
    }
}
