using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider1;
    public Slider audioSlider2;
    public Slider audioSlider3;

    public void MasterAudioControl()
    {
        float volum = audioSlider1.value;

        if(volum == -40)
        {
            audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            audioMixer.SetFloat("Master", volum);
        }
    }

    public void BGMAudioControl()
    {
        float volum = audioSlider2.value;

        if (volum == -40)
        {
            audioMixer.SetFloat("BGM", -80f);
        }
        else
        {

            audioMixer.SetFloat("BGM", volum);

        }
    }


    public void SFXAudioControl()
    {
        float volum = audioSlider3.value;

        if (volum == -40)
        {
            audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFX", volum);
        }
    }





}
