using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    AudioClip clip;
    string name;

    [HideInInspector]
    public AudioSource source;
}
