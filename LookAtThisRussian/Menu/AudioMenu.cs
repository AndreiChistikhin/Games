using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    AudioSource menuSong;
    float musicVolume = 1f;

    private void Start()
    {
        menuSong = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        menuSong.Play();
    }

    private void Update()
    {
        menuSong.volume = musicVolume;
    }

    //Adjust volume
    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }

    //Turn the music off/on
    public void SoundChange()
    {
        AudioSource song = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        if (song.isPlaying)
            song.Pause();
        else
            song.UnPause();
    }
}
