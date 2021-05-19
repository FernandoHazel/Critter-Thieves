using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] PlayerData playerData;
   public GameObject howToPlayMenu;
   //public GameObject Holder;

    //This void displays the "how to play" interface
   public void HowToPlay(){
      howToPlayMenu.SetActive(true);
   }

    public void LoadScene(string sceneToLoad){
      SceneManager.LoadScene(sceneToLoad);
   }

    public void ExitGame()
    {
      playerData.save();
      Application.Quit();
    }

    public void CloseHowToPlay(){
      howToPlayMenu.SetActive(false);
   }

}
