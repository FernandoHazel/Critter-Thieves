using UnityEngine;
using System.Collections;

public class ControllerColiderHit : MonoBehaviour
{ 

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
             body.velocity += hit.controller.velocity;
    }
}
