using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //We inport a library to manage scenes

public class ManageScene : MonoBehaviour
{
    //This void recieves the name of the scene to load
   public void LoadScene(string sceneToLoad){
       SceneManager.LoadScene(sceneToLoad);
   }
}
