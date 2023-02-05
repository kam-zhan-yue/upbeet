using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public const string APPLAUSE = "applause";
    public const string BAD = "bad";
    public const string OKAY = "okay";
    public const string PERFECT = "perfect";
    public const string HOLD = "hold";
    public const string BUTTON = "button";
    public const string ROOTS = "roots";
    
    public static AudioManager instance;

    public Sound[] sounds = Array.Empty<Sound>();

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        for (int i = 0; i < sounds.Length; ++i)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
            sounds[i].source.volume = sounds[i].volume;
        }
    }

    [Button]
    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (sounds[i].clip.name == _name)
            {
                // Debug.Log($"Play {_name}");
                sounds[i].source.Play();
            }
        }
    }

    [Button]
    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (sounds[i].clip.name == _name)
            {
                // Debug.Log($"Stop {_name}");
                sounds[i].source.Stop();
            }
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}