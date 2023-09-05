using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private DifficultySetting difficulty;
    
    public void OnButtonPressed()
    {
        SharedData.Difficulty = difficulty;
        if (PlayerPrefs.GetInt("PlayedTutorial", 0) == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void SetActive(GameObject obj)
    {
        obj.SetActive(true);
    }
    
    public void SetInactive(GameObject obj)
    {
        obj.SetActive(false);
    }
}
