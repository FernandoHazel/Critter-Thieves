using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] Image restartIcon;

    private bool click;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onButtonPress()
    {
        SceneManager.LoadScene("LevelBuilder");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
