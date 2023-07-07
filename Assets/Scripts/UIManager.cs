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
    [SerializeField] private TextMeshProUGUI answerText; // only on hard mode

    private String _interval;

    [Range(1f, 3f)]
    public float flashTime = 2f;

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
        EventManager.FlashAnswer += FlashAnswer;
        if (SharedData.Difficulty == DifficultySetting.Hard)
        {
            intervalText.gameObject.SetActive(false);
        }
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

    public void OnUpdateInterval(string interval, bool ascending)
    {
        var ascendText = ascending ? "Ascending" : "Descending";
        _interval = interval + " " + ascendText;
        intervalText.text = $"Interval: {interval} {ascendText}";
    }

    public void FlashAnswer(bool isCorrect)
    {
        StartCoroutine(FlashText(isCorrect, answerText));
    }

    private IEnumerator FlashText(bool correct, TMPro.TMP_Text text)
    {
        var counter = 0f;
        text.gameObject.SetActive(true);
        text.text = _interval;
        text.color = correct ? Color.green : Color.red;
        while (counter < flashTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        text.gameObject.SetActive(false);
    }
}
