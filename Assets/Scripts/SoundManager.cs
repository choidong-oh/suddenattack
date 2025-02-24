using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer1;
    public AudioMixer audioMixer2;
    public AudioMixer audioMixer3;
    public Slider audioSlider1;
    public Slider audioSlider2;
    public Slider audioSlider3;

    public void MasterAudioControl()
    {
        float volum = audioSlider1.value;

        if(volum == -40)
        {
            audioMixer1.SetFloat("Master", -80f);
        }
        else
        {
            audioMixer1.SetFloat("Master", volum);
        }
    }

    public void BGMAudioControl()
    {
        float volum = audioSlider2.value;

        if (volum == -40)
        {
            audioMixer2.SetFloat("BGM", -80f);
        }
        else
        {

            audioMixer2.SetFloat("BGM", volum);

        }
    }


    public void SFXAudioControl()
    {
        float volum = audioSlider3.value;

        if (volum == -40)
        {
            audioMixer3.SetFloat("SFX", -80f);
        }
        else
        {
            audioMixer3.SetFloat("SFX", volum);
        }
    }





}
