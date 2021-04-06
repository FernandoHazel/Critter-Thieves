using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    public void ChangeScene()
    {
        Debug.Log("Pico el boton");
        SceneManager.LoadScene("CritterThievesGame");
    }

    public void ExitGame()
    {
        Debug.Log("Pico el boton");
        Application.Quit();
    }

    void Update()
    {
        
    }
}
