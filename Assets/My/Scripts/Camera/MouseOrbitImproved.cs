using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public Transform target1;
    public float distance = 5.0f;
    float temp_distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;
    public float hitDistance = 0;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;
    float TargetX;
    bool IsZoom = false;
    // Use this for initialization
    void Start()
    {
        TargetX = target.position.x;


        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    private void Update()
    {
        if(Input.GetButtonUp("Fire2"))
        {
            if (IsZoom == false)
            {
                target.position = new Vector3(target.position.x + 0.5f, target.position.y, target.position.z+ 1);
                IsZoom = true;
            }
            else
            {
                target.position = new Vector3(target.position.x - 0.5f, target.position.y, target.position.z-1);
                IsZoom = false;
            }
        }
    }
    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
          
            Vector3 direction = target.position - transform.position;

            Debug.DrawRay(transform.position , direction, Color.red);

            if (Physics.Linecast(target.position,transform.position,  out hit))
            {
              if(hit.collider.tag!="Player" && hit.collider.tag != "CamRef")
                distance = hit.distance+0.2f;
            }

            direction = target1.position - transform.position;

            Debug.DrawRay(transform.position, direction, Color.red);

            if (Physics.Linecast(target1.position, transform.position, out hit))
            {
                if (hit.collider.tag != "Player" && hit.collider.tag != "CamRef")
                    distance = hit.distance + 0.2f;
            }

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}