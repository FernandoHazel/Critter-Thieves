using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapSoundManager : MonoBehaviour
{
    [SerializeField] Image soundOn;
    [SerializeField] Image soundOff;
    private bool mute;


    public static AudioClip angryCatSound;
    public static AudioClip sadCatSound;

    public static AudioClip fireSound;
    public static AudioClip trapSound;


    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fireSound = Resources.Load<AudioClip>("fire");
        trapSound = Resources.Load<AudioClip>("trap");





        audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("mute"))
        {
            PlayerPrefs.SetInt("mute", 0);
            Load();
        }

        else
        {
            Load();
        }

        updateButtonIcon();

        AudioListener.pause = mute;
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            

            case "fire":
                audioSource.PlayOneShot(fireSound);
                break;
            case "trap":
                audioSource.PlayOneShot(trapSound);
                break;


        }
    }

    public void onButtonPress()
    {
        if (mute == false)
        {
            mute = true;
            AudioListener.pause = true;
        }

        else
        {
            mute = false;
            AudioListener.pause = false;
        }

        Save();
        updateButtonIcon();
    }

    private void updateButtonIcon()
    {
        if (mute == false)
        {
            soundOn.enabled = true;
            soundOff.enabled = false;
        }

        else
        {
            soundOn.enabled = false;
            soundOff.enabled = true;
        }
    }

    private void Load()
    {
        mute = PlayerPrefs.GetInt("mute") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("mute", mute ? 1 : 0);
    }
}