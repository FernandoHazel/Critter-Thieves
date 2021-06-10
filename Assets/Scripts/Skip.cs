using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 34f;

    [SerializeField] private float delayBeforeText = 7f;

    public Text instruction;

    private float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        instruction.enabled = false;
    }

    public void SkipScene()
    {
        SceneManager.LoadScene("LevelBuilder");
    }

    void Intructions()
    {
        if (timeCounter > delayBeforeText)
        {
            instruction.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        Intructions();
          
        if(timeCounter > delayBeforeLoad)
        {
            SceneManager.LoadScene("LevelBuilder");
        }
    }
}
