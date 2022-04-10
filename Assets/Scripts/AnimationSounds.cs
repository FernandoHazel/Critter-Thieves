using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{

    private AudioSource footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep = GetComponent<AudioSource>();

    }

    public void Footstep()
    {
        footstep.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
