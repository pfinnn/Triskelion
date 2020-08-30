using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] clips_music;
    public Sound[] clips_UI;
    public Sound[] clips_effects;
    public Sound[] clips_ambient;

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
    }


}
