using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MainVolume", volume);
    }

    public void setFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
    
}
