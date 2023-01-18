using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumescript : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetVolume(float sliderValue)
    {
        //set audioMixer volume to slider value
        audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
    }
}
