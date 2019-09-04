using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource messageSound;
    public AudioSource buttonClickSound;
    public CustomSlider slider;
    
    private void Update()
    {
        messageSound.volume = slider.GetValue();
        buttonClickSound.volume = slider.GetValue();
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
