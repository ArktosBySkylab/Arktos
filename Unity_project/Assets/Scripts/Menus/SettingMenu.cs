using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.iOS;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()// à finir
    {
        Slider volumeController = GameObject.Find("Master/Volume").GetComponent<Slider>();
        Slider SoundEffectController = GameObject.Find("SoundEffect/SoundEffect").GetComponent<Slider>();
        Slider MusicController = GameObject.Find("Music/Music").GetComponent<Slider>();
        
        volumeController.value = PlayerPrefs.GetFloat(volumeController.name,0);
        audioMixer.SetFloat(volumeController.name, volumeController.value);
        
        SoundEffectController.value = PlayerPrefs.GetFloat(SoundEffectController.name,0);
        audioMixer.SetFloat(SoundEffectController.name, SoundEffectController.value);
        
        MusicController.value = PlayerPrefs.GetFloat(MusicController.name,0);
        audioMixer.SetFloat(MusicController.name, MusicController.value);
    }

    public void SetVolume(Slider slider)
    {
        audioMixer.SetFloat(slider.name, slider.value);
        PlayerPrefs.SetFloat(slider.name, slider.value);
    }
}
