using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Animator marcelAnimator;
    [SerializeField] public Transform charTranform;
    [SerializeField] Player player;
    float runMultiplier;
    void Start()
    {
        marcelAnimator = GetComponent<Animator>();
        charTranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        GrabFood();
        Debug.Log(runMultiplier);
        //Debug.Log(player.controller.isGrounded);
        //Run animation
        if (player.controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.D))
            {
                charTranform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                marcelAnimator.SetBool("Run", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                marcelAnimator.SetBool("Run", true);
                charTranform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
            }
            else
            {
                marcelAnimator.SetBool("Run", false);
            }
        }
        else
        {
            marcelAnimator.SetBool("Run", false);
        }
        

        //Jump animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            marcelAnimator.SetBool("Jump", true);
        }
        /*else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D))
        {
            marcelAnimator.SetBool("Jump", true);
            charTranform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A))
        {
            marcelAnimator.SetBool("Jump", true);
            charTranform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }*/
        else if (player.controller.isGrounded)
        {
            marcelAnimator.SetBool("Jump", false);
        }
        
        if (player.climb && Input.GetKey(KeyCode.W))
        {
            marcelAnimator.SetBool("Climb", true);
        }
        else if (!player.climb || player.controller.isGrounded)
        {
            marcelAnimator.SetBool("Climb", false);
        }
    }

    void GrabFood()
    {
        runMultiplier = 2 - player.Score * 0.3f;
        marcelAnimator.SetFloat("RunMultiplier", runMultiplier);
    }
}
