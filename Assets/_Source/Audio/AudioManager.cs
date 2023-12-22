using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public float SoundVolume { get; private set; }
    public float MusicVolume { get; private set; }

    [SerializeField] private Sound[] sounds;

    private Sound _currentMusic;
    private AudioSource _musicSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        _musicSource = gameObject.AddComponent<AudioSource>();
        foreach (Sound sound in sounds)
        {
            AddSound(sound);
        }

        SoundVolume = PlayerPrefs.GetFloat("SoundVolume",0.5f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume",0.5f);
        
        ChangeSoundVolume(SoundVolume);
        ChangeMusicVolume(MusicVolume);
    }
    
    public void ChangeSoundVolume(float volume)
    {
        foreach (Sound sound in sounds)
        {
            if(sound.isMusic) continue;
            sound.generalVolume = volume;
            sound.source.volume = sound.volume*sound.generalVolume;
        }
    }
    
    public void ChangeMusicVolume(float volume)
    {
        foreach (Sound sound in sounds)
        {
            if(!sound.isMusic) continue;
            sound.generalVolume = volume;
            if(sound.source!= null && sound.source.clip == sound.clip)
                sound.source.volume = sound.volume*sound.generalVolume;
        }
    }
    
    public void EnableSound(bool enable)
    {
        foreach (Sound sound in sounds)
        {
            if(sound.isMusic) continue;
            sound.source.mute = !enable;
        }
    }
    
    public void EnableMusic(bool enable)
    {
        foreach (Sound sound in sounds)
        {
            if(!sound.isMusic) continue;
            sound.source.mute = !enable;
        }
    }
    
    private void AddSound(Sound sound)
    {
        sound.source = sound.isMusic ? _musicSource : gameObject.AddComponent<AudioSource>();
        if (sound.playOnAwake)
        {
            Play(sound);
        }
    }
    
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
        {
            Play(sound);
        }
        else
            Debug.LogWarning("Sound " + name + " not found");
    }
    
    public void Play(Sound sound)
    {
        if (sound.isMusic)
        {
            if(sound.clip == _musicSource.clip)
                return;
            sound.source = _musicSource;
            sound.source.volume = sound.volume*sound.generalVolume;
        }
        sound.Play();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.Save();
    }
}
