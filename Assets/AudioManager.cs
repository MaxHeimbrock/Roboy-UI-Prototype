using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource messageSound;
    public AudioSource buttonClickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMessageSound()
    {
        messageSound.Play();
    }

    public void PlayButtonClickSound()
    {
        buttonClickSound.Play();
    }
}
