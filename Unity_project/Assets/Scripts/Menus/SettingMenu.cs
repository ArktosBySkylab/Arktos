using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.iOS;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeController;

    private void Start()
    {
        audioMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("sound"));
        volumeController.value = PlayerPrefs.GetFloat("sound");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
        PlayerPrefs.SetFloat("sound", volume);
    }
    
}
