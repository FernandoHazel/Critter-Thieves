using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOn;
    [SerializeField] Image soundOff;
    private bool mute;

    public static AudioClip grabFoodSound;
    public static AudioClip dropFoodSound;
    public static AudioClip damageSound;
    public static AudioClip glueTrapSound;
    public static AudioClip deliverFoodSound;
    public static AudioClip questCompletedSound;
    public static AudioClip angryCatSound;
    public static AudioClip sadCatSound;
    public static AudioClip jumpSound;
    public static AudioClip landingSound;
    public static AudioClip levelCompletedSound;
    public static AudioClip stepSound;
    public static AudioClip dieSound;
    public static AudioClip fireSound;
    public static AudioClip trapSound;


    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        grabFoodSound = Resources.Load<AudioClip>("grabFood");
        dropFoodSound = Resources.Load<AudioClip>("dropFood");
        damageSound = Resources.Load<AudioClip>("damage");
        dieSound = Resources.Load<AudioClip>("fire");
        dieSound = Resources.Load<AudioClip>("trap");
        glueTrapSound = Resources.Load<AudioClip>("glueTrap");
        deliverFoodSound = Resources.Load<AudioClip>("deliverFood");
        questCompletedSound = Resources.Load<AudioClip>("questCompleted");
        angryCatSound = Resources.Load<AudioClip>("angryCat");
        sadCatSound = Resources.Load<AudioClip>("sadCat");
        jumpSound = Resources.Load<AudioClip>("jump");
        landingSound = Resources.Load<AudioClip>("landing");
        levelCompletedSound = Resources.Load<AudioClip>("levelCompleted");
        stepSound = Resources.Load<AudioClip>("step");
        dieSound = Resources.Load<AudioClip>("die");




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
        switch(clip)
        {
            case "grabFood":
                audioSource.PlayOneShot(grabFoodSound);
                break;

            case "dropFood":
                audioSource.PlayOneShot(dropFoodSound);
                break;

            case "damage":
                audioSource.PlayOneShot(damageSound);
                break;

            case "glueTrap":
                audioSource.PlayOneShot(glueTrapSound);
                break;

            case "deliverFood":
                audioSource.PlayOneShot(deliverFoodSound);
                break;

            case "questCompleted":
                audioSource.PlayOneShot(questCompletedSound);
                break;

            case "angryCat":
                audioSource.PlayOneShot(angryCatSound);
                break;

            case "sadCat":
                audioSource.PlayOneShot(sadCatSound);
                break;

            case "jump":
                audioSource.PlayOneShot(jumpSound);
                break;

            case "landing":
                audioSource.PlayOneShot(landingSound);
                break;

            case "levelCompleted":
                audioSource.PlayOneShot(levelCompletedSound);
                break;

            case "fire":
                audioSource.PlayOneShot(fireSound);
                break;
            case "trap":
                audioSource.PlayOneShot(trapSound);
                break;

            case "step":
                audioSource.PlayOneShot(stepSound);
                break;

            case "die":
                audioSource.PlayOneShot(dieSound);
                break;

        }
    }    

    public void onButtonPress()
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

        Save();
        updateButtonIcon();
    }

    private void updateButtonIcon()
    {
        if ( mute == false)
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
