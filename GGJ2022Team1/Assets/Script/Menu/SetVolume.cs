using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixerGroup mixer;                                                    //mixer dei suoni
    public AudioMixerGroup mixer2;                                                    //mixer dei suoni

    public void SetLevel(float sliderValue)
    {
        mixer.audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);           //setto lo slider nel menu
    }

    public void SetLevel2(float sliderValue)
    {
       mixer2.audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);           //setto lo slider nel menu
    }

}

