using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public SoundStore[] musicSounds, sfxSounds;
    public AudioSource musicSource,sfxSource;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        SoundStore sound = Array.Find(musicSounds, x => x.name==name);
        if(sound == null)
        {
            Debug.Log("Sound not found");
            musicSource.Stop();
        }
        else
        {
            musicSource.clip = sound.audioClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        SoundStore sound = Array.Find(sfxSounds, x=> x.name==name);
        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.clip = sound.audioClip;
            sfxSource.PlayOneShot(sound.audioClip);
        }
    }

}
