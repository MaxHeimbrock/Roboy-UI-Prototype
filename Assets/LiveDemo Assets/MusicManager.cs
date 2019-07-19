using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    AudioSource[] audioSources;
    float[] resumeTimer;
    bool allQuiet;

    CustomSlider sliderAG;
    CustomSlider sliderBass;
    CustomSlider sliderBCV;
    CustomSlider sliderEG;
    CustomSlider sliderDrums;
    CustomSlider sliderLV;
    CustomSlider sliderPiano;
    CustomSlider sliderStringsRodes;



    // Start is called before the first frame update
    private void Start()
    {
        Setup();
    }
    public void Setup()
    {
        audioSources = GetComponents<AudioSource>();
        resumeTimer = new float[audioSources.Length];
        allQuiet = true;

        sliderAG = GameObject.FindGameObjectWithTag("AG").GetComponent<CustomSlider>();
        sliderBass = GameObject.FindGameObjectWithTag("Bass").GetComponent<CustomSlider>();
        sliderBCV = GameObject.FindGameObjectWithTag("BCV").GetComponent<CustomSlider>();
        sliderDrums = GameObject.FindGameObjectWithTag("Drums").GetComponent<CustomSlider>();
        sliderEG = GameObject.FindGameObjectWithTag("EG").GetComponent<CustomSlider>();
        sliderLV = GameObject.FindGameObjectWithTag("LV").GetComponent<CustomSlider>();
        sliderPiano = GameObject.FindGameObjectWithTag("Piano").GetComponent<CustomSlider>();
        sliderStringsRodes = GameObject.FindGameObjectWithTag("StringsRodes").GetComponent<CustomSlider>();

        activateMusic();
    }

    // Update is called once per frame
    void Update()
    {
        audioSources[0].volume = sliderAG.GetValue();
        audioSources[1].volume = sliderBass.GetValue();
        audioSources[2].volume = sliderBCV.GetValue();
        audioSources[3].volume = sliderDrums.GetValue();
        audioSources[4].volume = sliderEG.GetValue();
        audioSources[5].volume = sliderLV.GetValue();
        audioSources[6].volume = sliderPiano.GetValue();
        audioSources[7].volume = sliderStringsRodes.GetValue();

        foreach(AudioSource audio in audioSources)
        {
            if (audio.isPlaying)
            {
                allQuiet = false;
                break;
            }
        }
        if (allQuiet)
        {
            activateMusic();
        }
        allQuiet = true;
    }

    public void startMusic()
    {
        /*for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].UnPause();
            //audioSources[i].time = resumeTimer[i];
        }*/

        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }
    }

    public void stopMusic()
    {
        /*for (int i = 0; i < audioSources.Length; i++)
        {
            //resumeTimer[i] = audioSources[i].time;
            audioSources[i].Pause();
        }*/

        foreach(AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    public void muteOn(int track)
    {
        audioSources[track].mute = true;
    }

    public void muteOff(int track)
    {
        audioSources[track].mute = false;
    }

    private void activateMusic()
    {
        foreach(AudioSource audio in audioSources)
        {
            audio.Play();
        }
    }
}
