using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicID = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[musicID];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicID = (musicID + 1) % playlist.Length;
        audioSource.clip = playlist[musicID];
        audioSource.Play();
    }
}
