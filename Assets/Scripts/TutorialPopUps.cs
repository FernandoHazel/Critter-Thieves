using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUps : MonoBehaviour
{
    private new GameObject gameObject;
    //popUp = gameObject.GetComponent<>

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PopUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PopUp.SetActive(false);
        }
    }
}

