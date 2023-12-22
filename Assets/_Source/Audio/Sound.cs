using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class Sound
{
    public string name;

    [SerializeField] private AudioClip[] clips;
    [HideInInspector]
    public AudioClip clip
    {
        get {return clips[UnityEngine.Random.Range(0, clips.Length)];}
    }

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0f, 1f)]
    public float generalVolume = 1;
    [Range(0.1f, 3f)]
    public float pitch = 1;
    [Range(0f, 1f)]
    public float pitch_variation = 0;

    public bool loop;
    public bool isMusic;
    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;
    
    public void Play()
    {
        source.clip = clip;
        source.volume = volume*generalVolume;
        source.loop = loop;
        source.pitch = Mathf.Clamp( pitch + UnityEngine.Random.Range(-pitch_variation, pitch_variation), 0.1f, 3f);
        source.Play();
    }
}
