using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Player playerScript;
    [SerializeField] BoxCollider boxCollider;


    private void Start() 
    {
        boxCollider = GetComponent<BoxCollider>();
        //Debug.Log("Running Platform Script");
        boxCollider.enabled = false;
    }
    void Update()
    {
        //bool climbHere = playerScript.climb;
        //bool climbJumpHere = playerScript.climbJump;
        if (playerScript.climb || playerScript.climbJump)
        {
            Debug.Log("Must desactivate collider");
            boxCollider.enabled = false;
        }
        /*if (climbHere || climbJumpHere)
        {
            Debug.Log("Must desactivate collider");
            boxCollider.enabled = false;
        }*/
        //boxCollider.enabled = true;
    }
}
