using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPopUps : MonoBehaviour
{
    public GameObject F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            F.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){
            F.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
