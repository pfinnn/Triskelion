using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] music;
    public Sound[] UI;
    public Sound[] effects;
    public Sound[] ambient;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        // Singleton Pattern
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        AddSoundsToSources();

    }

    private void Start()
    {
        PlayMusic("Wanderer");
    }

    private void AddSoundsToSources()
    {
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        foreach (Sound s in UI)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        foreach (Sound s in effects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        foreach (Sound s in ambient)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void PlayMusic (string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name: "+name+ " was not found!");
        }
        s.source.Play();
    }

    public void PlayAmbient(string name)
    {
        Sound s = Array.Find(ambient, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name: " + name + " was not found!");
        }
        s.source.Play();
    }

    public void PlayUIEffect(string name)
    {
        Sound s = Array.Find(UI, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name: " + name + " was not found!");
        }
        s.source.Play();
    }

    public void PlayEffect(string name)
    {
        Sound s = Array.Find(effects, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name: " + name + " was not found!");
        }
        s.source.Play();
    }

}
