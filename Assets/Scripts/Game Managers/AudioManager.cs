using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] clips_music;
    public Sound[] clips_UI;
    public Sound[] clips_effects;
    public Sound[] clips_ambient_fields;
    public Sound[] clips_ambient_city;

    public AudioSource ambient_City;
    public AudioSource ambient_Field;
    public AudioSource music;

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

    }

    public void Start()
    {
        music.clip = clips_music[0].clip;
        music.Play();

        ambient_City.clip = clips_ambient_city[0].clip;
        ambient_City.Play();

        ambient_Field.clip = clips_ambient_fields[0].clip;
        ambient_Field.Play();

    }


}
