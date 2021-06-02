using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOn;
    [SerializeField] Image soundOff;
    private bool mute;

    // Start is called before the first frame update
    void Start()
    {

    }

    void onButtonPress()
    {
        if(mute == false)
        {
            mute = true;
            AudioListener.pause = true;
        }

        else
        {
            mute = false;
            AudioListener.pause = false;
        }
    }

    private void Load()
    {
        
    }

    private void Save()
    {

    }
}
