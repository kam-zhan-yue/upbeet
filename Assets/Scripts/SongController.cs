using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public SongPlayer songPlayer;
    public NotePlayer notePlayer;

    public void Init(BeatMap _beatMap)
    {
        songPlayer.Init(_beatMap);
        notePlayer.Init(_beatMap);
    }
    
    public void Play(int _beat = 0)
    {
        songPlayer.Play(_beat);
        notePlayer.Play(_beat);
    }

    public void Pause()
    {
        songPlayer.Pause();
        notePlayer.Pause();
    }

    public void Resume()
    {
        songPlayer.Resume();
        notePlayer.Resume();
    }

    public void Stop()
    {
        songPlayer.Stop();
        notePlayer.Stop();
    }
}
