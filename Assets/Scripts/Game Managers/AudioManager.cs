using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Clip Arrays
    public AudioClip[] clips_music;
    public AudioClip[] clips_UI;
    public AudioClip[] clips_effects;
    public AudioClip[] clips_ambient_fields;
    public AudioClip[] clips_ambient_city;

    public AudioSource UI_source;
    public AudioSource ambient_City;
    public AudioSource ambient_Field;
    public AudioSource music;


    public static AudioManager instance;


    public enum UISoundTypes
    {
        ButtonClicked,
    }


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

    }

    public void Start()
    {
        music.clip = clips_music[0];
        music.Play();

        ambient_City.clip = clips_ambient_city[0];
        ambient_City.Play();

        ambient_Field.clip = clips_ambient_fields[0];
        ambient_Field.Play();

    }

    public void PlayUISound(UISoundTypes soundType)
    {
        switch (soundType)
        {
            case UISoundTypes.ButtonClicked:
                UI_source.clip = clips_UI[0];
                UI_source.Play();
                break;
        }
    }  


}
