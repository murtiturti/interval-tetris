using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private int _score;
    private int _highScore;
    private const string HighScoreKey = "HighScore";

    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private GridManager gridManager;
    
    public CustomIntEvent scoreChanged, highScoreChanged;
    public UnityEvent gameOverEvent;

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
    
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        EventManager.ReadyForSpawn += OnSpawnReady;
    }

    // Update is called once per frame
    void Update()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
            highScoreChanged.Invoke(_highScore);
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

    private void OnSpawnReady()
    {
        var note = PickNote();
        var freq = GetFrequencyOf(note);
        var interval = "";
        var freq2 = 0f;
        var note2 = "";
        IntervalGenerator.ChooseInterval(out interval, freq, out freq2, note, out note2);
        var spawned = spawner.Spawn(gridManager.Grid.GetCellWorldPosition(2, 5), note2, note);
        AudioClip firstClip = null;
        AudioClip secondClip = null;
        IntervalGenerator.CreateClip(freq, out firstClip);
        IntervalGenerator.CreateClip(freq2, out secondClip);
        IntervalPlayer.Instance.SetFirstClip(firstClip);
        IntervalPlayer.Instance.SetSecondClip(secondClip);
        gridManager.SetLastSpawned(spawned);
        IntervalPlayer.Instance.PlayInterval(true);
    }

    public void AddScore(int score)
    {
        _score += score;
        scoreChanged.Invoke(_score);
    }

    public void OnGameOver()
    {
        gameOverEvent.Invoke();
        PlayerPrefs.SetInt(HighScoreKey, _highScore);
    }
}
