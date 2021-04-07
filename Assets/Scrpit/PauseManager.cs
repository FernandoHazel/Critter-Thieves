using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //We inport a library to manage scenes

public class PauseManager : MonoBehaviour
{
   //this variables holds the different canvas objects of the pause and the running game
   public GameObject inGame;
   public GameObject inPause;
   //We take the game object "How to play"
   public GameObject HowToPlay;
    

   //This void displays the "how to play" interface
   public void howToPlay(){

      //First we desactivate the rest of the elements of the canvas
      inGame.SetActive(false);
      inPause.SetActive(false);

      //We activate the "How to play" game object
      HowToPlay.SetActive(true);
   }
    
   //This void recieves the name of the scene to load
   public void LoadScene(string sceneToLoad){
      SceneManager.LoadScene(sceneToLoad);
   }

    //This void quits the game
   public void Exit(){
      Application.Quit();
      Debug.Log("Quit");
   }

    //This void resumes the game
   public void resume(){
      GameManager.pause=false;
   }
    
}
