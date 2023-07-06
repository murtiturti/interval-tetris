using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI intervalText;

    public enum UpdateType
    {
        Score,
        HighScore
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
        EventManager.GameOver += OnGameOver;
        EventManager.UpdateUI += UpdateUI;
        EventManager.UpdateInterval += OnUpdateInterval;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
    private void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void UpdateHighScore(int newScore)
    {
        highScoreText.text = $"High Score: {newScore}";
    }

    public void UpdateUI(int newScore, UpdateType type)
    {
        if (type == UpdateType.Score)
        {
            UpdateScore(newScore);
        }
        else
        {
            UpdateHighScore(newScore);
        }
    }

    public void OnUpdateInterval(string interval)
    {
        intervalText.text = $"Interval: {interval}";
    }
}
