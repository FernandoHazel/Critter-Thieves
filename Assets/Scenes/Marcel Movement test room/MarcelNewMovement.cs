using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelNewMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    /*
    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        transform.Translate(horizontal * Time.deltaTime * 10, 10, 0);
    }
    */
    /*
    private void OnTriggerStay(Collider other)  //Tags
    {

        if (other.gameObject.tag == "Climb")
        {
            Debug.Log("Lo está tocando");
            float vertical = Input.GetAxis("Vertical");
            transform.Translate(vertical * Time.deltaTime * 0, 10, 0);
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        //movement();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        characterController.SimpleMove(new Vector3(h * speed, 0, v * speed));
    }
}
