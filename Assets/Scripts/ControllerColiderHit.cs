using UnityEngine;
using System.Collections;

public class ControllerColiderHit : MonoBehaviour
{

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            if (Input.GetKey(KeyCode.F))
            {

                body.velocity += (hit.controller.velocity * .1666f);
            }
        }
    }
}
