using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    int songNumber;
    bool musciIsOn;
    float musicVolume=1f;
   
    void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source= gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
        }
      //Play random song
      songNumber = UnityEngine.Random.Range(3,8);
      musciIsOn = true;
      Play("Song"+songNumber);
    }

    public void Play(string name) 
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
     private void Update()
      {
        foreach (Sound s in sounds)
        {
            s.source.volume = musicVolume;
        }
        //Play next song
        if (!sounds[songNumber].source.isPlaying&&musciIsOn)
          {
              songNumber++;
              if (songNumber != 8)
              {
                  Play("Song" + songNumber);
              }
              else
              {
                  songNumber = 3;
                  Play("Song" + songNumber);
              }
          }
      }

    //Turn the music off/on 
    public void MusicTrigger()
    {
        if (musciIsOn == true)
        {
            foreach (Sound s in sounds)
            {
                musciIsOn = false;
                s.source.Stop();
            }
        }  
        else
        {
            songNumber = UnityEngine.Random.Range(3, 8);
            musciIsOn = true;
            Play("Song" + songNumber);
        }
                
            
    }

    public void VolumeChange(float volume)
    {
        musicVolume = volume;
    }

      

}
