using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //[SerializeField] Player playerScript;
    [SerializeField] BoxCollider boxCollider;

    public bool climbHere;

    public bool climbJumpHere;

    private void Start() 
    {
        boxCollider = GetComponent<BoxCollider>();
        //Debug.Log("Running Platform Script");
    }
    void Update()
    {
        //bool climbHere = playerScript.climb;
        //bool climbJumpHere = playerScript.climbJump;
        /*if (playerScript.climb || playerScript.climbJump)
        {
            Debug.Log("Must desactivate collider");
            boxCollider.enabled = false;
        }*/
        if (climbHere || climbJumpHere)
        {
            Debug.Log("Must desactivate collider");
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
        //boxCollider.enabled = true;
    }
}
