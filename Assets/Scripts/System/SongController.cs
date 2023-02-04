using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SongController : MonoBehaviour
{
    [Serializable]
    public class NotePlayerEntry
    {
        public int lanes;
        public NotePlayer notePlayer;
    }

    public SongPlayer songPlayer;
    public ScoreController scoreController;
    public NoteFactory noteFactory;
    public SaveController saveController;

    public BoolReference gamePlaying;

    [TableList] 
    public List<NotePlayerEntry> notePlayerPrefabs = new();
    private List<NotePlayerEntry> notePlayerList = new();

    private NotePlayer currentNotePlayer;

    private void Awake()
    {
        for (int i = 0; i < notePlayerPrefabs.Count; ++i)
        {
            NotePlayer player = Instantiate(notePlayerPrefabs[i].notePlayer, transform);
            NotePlayerEntry entry = new();
            entry.lanes = notePlayerPrefabs[i].lanes;
            entry.notePlayer = player;
            notePlayerList.Add(entry);
            player.gameObject.SetActiveFast(false);
        }
        gamePlaying.Value = false;
    }

    public void Init(BeatMap _beatMap)
    {
        songPlayer.Init(_beatMap);
        int lanes = _beatMap.lanes;
        NotePlayer notePlayer = GetPlayer(lanes);
        SetCurrentPlayer(notePlayer);
        currentNotePlayer.Init(noteFactory, _beatMap);
        scoreController.Init();
    }

    private void SetCurrentPlayer(NotePlayer _notePlayer)
    {
        if(currentNotePlayer != null)
            currentNotePlayer.gameObject.SetActiveFast(false);
        currentNotePlayer = _notePlayer;
        currentNotePlayer.gameObject.SetActiveFast(true);
    }

    private void Update()
    {
        if (gamePlaying)
        {
            //If game is going on, but not audio and no pause, then report game completed
            if (songPlayer.NotPlayingAndNotPaused)
            {
                Debug.Log("Game Completed.");
                gamePlaying.Value = false;
                ScoreSaveData saveData = scoreController.ProcessResults(currentNotePlayer.BeatMap);
                if(saveController != null)
                    saveController.Save(saveData);
                if(PopupManager.instance != null)
                    PopupManager.instance.ShowResults();
            }
        }
    }

    public void Play(int _beat = 0)
    {
        songPlayer.Play(_beat);
        currentNotePlayer.Play(_beat);
        gamePlaying.Value = true;
    }

    public void Pause()
    {
        songPlayer.Pause();
        currentNotePlayer.Pause();
    }

    public void Resume()
    {
        songPlayer.Resume();
        currentNotePlayer.Resume();
    }

    public void Restart()
    {
        Stop();
        Init(currentNotePlayer.BeatMap);
        Play();
    }

    public void Stop()
    {
        songPlayer.Stop();
        currentNotePlayer.Stop();
        scoreController.UnInit();
        gamePlaying.Value = false;
    }

    private NotePlayer GetPlayer(int _lanes)
    {
        for (int i = 0; i < notePlayerList.Count; ++i)
        {
            if (notePlayerList[i].lanes == _lanes)
                return notePlayerList[i].notePlayer;
        }
        
        Debug.LogWarning("No note player with correct lanes!");
        
        for (int i = 0; i < notePlayerList.Count; ++i)
        {
            if (notePlayerList[i].lanes > _lanes)
                return notePlayerList[i].notePlayer;
        }
        Debug.LogError("No note player with sufficient lanes!");
        return null;
    }

    public void CloseNotePlayer()
    {
        currentNotePlayer.gameObject.SetActiveFast(false);
    }
}
