using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    
    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField] private GameObject titleTxtGameObject;
    [SerializeField] private GameObject subtitleTxtGameObject;
    [SerializeField] private List<TutorialData> tutorialDataList = new List<TutorialData>();
    [SerializeField] private GameObject startCountdownTxtGameObject;
    private int _tutorialIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PlayedTutorial", 1);
        EventManager.NextTutorial += NextTutorial;
        StartCoroutine(StartTutorial());
    }

    private void Update()
    {
        if (_tutorialIndex == 4)
        {
            EventManager.NextTutorial -= NextTutorial;
            _tutorialIndex++;
            StartCoroutine(StartCountdown());
        }
    }


    private void NextTutorial()
    {
        tutorialPanel.SetActive(true);
        titleTxtGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = tutorialDataList[_tutorialIndex].title;
        subtitleTxtGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = tutorialDataList[_tutorialIndex].subtitle;
        EventManager.OnGamePaused(true);
    }

    public void Unpause()
    {
        EventManager.OnGamePaused(false);
    }
    
    public void IncreaseTutorialIndex()
    {
        _tutorialIndex++;
    }

    private IEnumerator StartTutorial()
    {
        yield return new WaitForEndOfFrame();
        NextTutorial();
    }

    private IEnumerator StartCountdown()
    {
        startCountdownTxtGameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            startCountdownTxtGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Starting in {i}...";
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(1);
    }
    
}
