using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 36f;

    private float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SkipScene()
    {
        SceneManager.LoadScene("LevelBuilder");
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;  

        if(timeCounter > delayBeforeLoad)
        {
            SceneManager.LoadScene("LevelBuilder");
        }
    }
}
