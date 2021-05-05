using UnityEngine;


public class TFScamera : MonoBehaviour
{
    public float speed = 1f;
    public Transform target;
    public Camera cam;

    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = (target.position - cam.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = transform.rotation.x;
        lookRotation.z = transform.rotation.z;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 500);
        transform.position = Vector3.Slerp(transform.position, target.position, Time.deltaTime * speed);
        transform.LookAt(target);
    }
}