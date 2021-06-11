using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTypeWrittingEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    private float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().enabled = false;
        StartCoroutine(ShowText());


    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(5f);
        this.GetComponent<Text>().enabled = true;
        StartCoroutine(WriteText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(5f);
        this.GetComponent<Text>().enabled = false;

    }
    IEnumerator WriteText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(HideText());
    }

}
