using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        foreach (Sound sound in sounds)
        {
            AddSound(sound);
        }
    }

    public void ChangeSoundVolume(float volume)
    {
        foreach (Sound sound in sounds)
        {
            if(sound.isMusic) return;
            sound.volume = volume;
            sound.source.volume = volume;
        }
    }
    
    public void ChangeMusicVolume(float volume)
    {
        foreach (Sound sound in sounds)
        {
            if(!sound.isMusic) return;
            sound.volume = volume;
            sound.source.volume = volume;
        }
    }
    
    private void AddSound(Sound sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
        if (sound.playOnAwake)
            Play(sound);
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
            Play(sound);
        else
            Debug.LogWarning("Sound " + name + " not found");

    }
    public void Play(Sound sound)
    {
        sound.Play();
    }

}
