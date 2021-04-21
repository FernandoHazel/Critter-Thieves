using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPopUps : MonoBehaviour
{
    public GameObject interactionButtonF;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            interactionButtonF.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){
            interactionButtonF.SetActive(false);
        }
    }
}
