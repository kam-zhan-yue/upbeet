using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private BeatMap beatMap;
    
    //Current song position, in seconds
    [NonSerialized, ShowInInspector, ReadOnly]
    private float songPosition = 0f;
    
    //Current song position, in beats
    [NonSerialized, ShowInInspector, ReadOnly]
    private float songPositionInBeats = 0f;
    
    //How many seconds have passed since the song started
    [NonSerialized, ShowInInspector, ReadOnly]
    private float dspSongTime = 0f;
    
    //The current beat of the song
    [NonSerialized, ShowInInspector, ReadOnly]
    private int currentBeat = 0;
    private int previousBeat = 0;
    private int beatOffset = 0;
    private bool playing = false;

    public int CurrentBeat => currentBeat;

    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Init(BeatMap _beatMap)
    {
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        beatMap = _beatMap;
        audioSource.clip = beatMap.song;
    }

    private void Update()
    {
        Tick();
    }

    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Play(int _beat = 0)
    {
        if (beatMap == null)
        {
            Debug.LogError("No beat map attached to this song player.");
            return;
        }
        float songTime = _beat * beatMap.SecPerBeat;
        audioSource.time = songTime;
        audioSource.Play();
        beatOffset = _beat;
        
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        playing = true;
    }
    
    public void Tick()
    {
        if (!playing)
            return;
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / beatMap.SecPerBeat;
        songPositionInBeats += beatOffset;
        
        currentBeat = (int)(songPositionInBeats);
        if (currentBeat > previousBeat)
        {
            previousBeat = currentBeat;
        }
    }

    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Pause()
    {
        audioSource.Pause();
        playing = false;
    }
    
    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Resume()
    {
        audioSource.Play();
        playing = true;
    }

    [FoldoutGroup("Editor Functions")]
    [Button]
    public void Stop()
    {
        audioSource.Stop();
        playing = false;
    }
}
