using UnityEngine;

public  class CameraClippingDitector : MonoBehaviour
{
    public float distance = 5;
    public float R_distance = 5;
    public float L_distance = 5;
    private Vector3 initPosition;
    private void Start()
    {
        initPosition = transform .position.normalized ;
    }
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            distance = hit.distance;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right), Color.yellow);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
        {
            if (hit.collider.tag != TagManager.Player)
            {
                R_distance = hit.distance;
            }
        }
        Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.right), Color.blue);
        if (Physics.Raycast(transform.position,-transform.TransformDirection(Vector3.right), out hit))
        {
            if (hit.collider.tag != TagManager.Player)
            {
                L_distance = hit.distance;
            }

        }

        //if (R_distance < 1 || L_distance < 1)
        //{
        //    transform.position = new Vector3(0, transform.position.y, transform.position.z).normalized ;
        //}
        //else
        //{
        //    transform.position = initPosition.normalized ;
        //}

    }
}