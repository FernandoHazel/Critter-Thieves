using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Push()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && Input.GetKeyDown(KeyCode.F))
        {
            box = hit.collider.gameObject;
            box.GetComponent<FixedJoint>().enableCollision = true;
            box.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
