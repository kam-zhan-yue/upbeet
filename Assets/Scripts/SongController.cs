using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public SongPlayer songPlayer;

    public void Init(BeatMap _beatMap)
    {
        songPlayer.Init(_beatMap);
    }
    
    public void Play(int _beat = 0)
    {
        songPlayer.Play(_beat);
    }

    public void Pause()
    {
        songPlayer.Pause();
    }

    public void Resume()
    {
        songPlayer.Resume();
    }

    public void Stop()
    {
        songPlayer.Stop();
    }
}
