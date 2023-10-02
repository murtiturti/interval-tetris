using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    private int _score;
    private int _highScore;
    private const string HighScoreKey = "HighScore";

    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private GridManager gridManager;

    private Block _lastSpawned;

    private bool _ascending;
    
    private DifficultySetting _difficulty = SharedData.Difficulty;

    private string[] _noteNames = new[] {"A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab"};
    private Dictionary<string, float> _noteFrequencies = new Dictionary<string, float>()
    {
        {"A", 220f},
        {"Bb", 233.08f},
        {"B", 246.94f},
        {"C", 261.63f},
        {"Db", 277.18f},
        {"D", 293.66f},
        {"Eb", 311.13f},
        {"E", 329.63f},
        {"F", 349.23f},
        {"Gb", 369.99f},
        {"G", 392.00f},
        {"Ab", 415.30f}
    };

    private void Awake()
    {
        EventManager.ReadyForSpawn += OnSpawnReady;
        EventManager.GameOver += OnGameOver;
        EventManager.ScoreChanged += AddScore;
        EventManager.SetLastSpawned += SetLastSpawned;
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        OnSpawnReady();
    }

    // Update is called once per frame
    void Update()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
        }
    }
    
    private string PickNote()
    {
        return _noteNames[Random.Range(0, _noteNames.Length)];
    }

    private float GetFrequencyOf(string note)
    {
        return _noteFrequencies[note];
    }

    private void SetLastSpawned()
    {
        gridManager.SetLastSpawned(this._lastSpawned);
    }

    private void OnSpawnReady()
    {
        IntervalPlayer.Instance.StopInterval();
        _ascending = _difficulty == DifficultySetting.Easy ? true : Utilities.RandomBool();
        IntervalPicker.PickNotes(out var note1, out var note2, out var note1Octave, out var note2Octave, out var interval, _ascending, _difficulty);
        var spawned = spawner.Spawn(gridManager.Grid.GetCellWorldPosition(2, 5), note2, note1);
        this._lastSpawned = spawned;
        var firstClip = NoteLoader.LoadAudioClip(note1 + note1Octave);
        var secondClip = NoteLoader.LoadAudioClip(note2 + note2Octave);
        IntervalPlayer.Instance.SetFirstClip(firstClip);
        IntervalPlayer.Instance.SetSecondClip(secondClip);
        gridManager.SetLastSpawned(spawned);
        StartCoroutine(IntervalPlayer.Instance.Play());
        gridManager.SetAscending(_ascending);
        EventManager.UpdateIntervalUI(interval, _ascending);
    }

    public void AddScore(int score)
    {
        _score += score;
        // Send message to UI to update score
        EventManager.OnUpdateUI(_score, UIManager.UpdateType.Score);
        if (_score > _highScore)
        {
            _highScore = _score;
            EventManager.OnUpdateUI(_highScore, UIManager.UpdateType.HighScore);
        }
    }

    private void OnGameOver()
    {
        Debug.Log("Game over");
        PlayerPrefs.SetInt(HighScoreKey, _highScore);
        gridManager.GameOver();
    }
}
