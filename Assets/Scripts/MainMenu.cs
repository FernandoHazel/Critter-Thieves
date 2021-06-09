using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] PlayerData playerData;
   public GameObject howToPlayMenu;
   public GameObject credits;
    //public GameObject Holder;

    //This void displays the "how to play" interface
    public void HowToPlay(){
      howToPlayMenu.SetActive(true);
   }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void LoadScene(string sceneToLoad){
      SceneManager.LoadScene(sceneToLoad);
      PlayerPrefs.DeleteAll();
   }

    public void ExitGame()
    {
      Application.Quit();
    }

    public void CloseHowToPlay(){
      howToPlayMenu.SetActive(false);
   }

    public void CloseCredits()
    {
        credits.SetActive(false);
    }

}
