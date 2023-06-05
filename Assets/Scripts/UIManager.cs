using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateHighScore(int newScore)
    {
        highScoreText.text = $"High Score: {newScore}";
    } 
}
