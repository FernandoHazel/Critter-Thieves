using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Animator animator;
    [SerializeField] public Transform charTranform;
    [SerializeField] Player player;
    void Start()
    {
        animator = GetComponent<Animator>();
        charTranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Presionando D");
            charTranform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Presionando A");
            animator.SetBool("Run", true);
            charTranform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Presionando A");
            animator.SetBool("Run", true);
            charTranform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}
