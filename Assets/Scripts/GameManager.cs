using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private int _score;
    private int _highScore;
    private const string HighScoreKey = "HighScore";
    
    public CustomIntEvent scoreChanged, highScoreChanged;
    public UnityEvent gameOverEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
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
