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

    private void AddSound(Sound sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
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
