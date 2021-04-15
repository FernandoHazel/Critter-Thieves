using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public GameObject HowToPlay_M;
   //public GameObject Holder;

    //This void displays the "how to play" interface
   public void howToPlay(){
      HowToPlay_M.SetActive(true);
   }

    public void LoadScene(string sceneToLoad){
      SceneManager.LoadScene(sceneToLoad);
   }

    public void ExitGame()
    {
        Debug.Log("Pico el boton");
        Application.Quit();
    }

    public void closeHowToPlay(){
      HowToPlay_M.SetActive(false);
   }

}
