using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public GameObject levelFailedUI;
    public GameObject tapToPlayGameObject;

    public Button pressToPlayButton;

    private void Start()
    {
        pressToPlayButton.onClick.AddListener(OnPressToPlayButtonClicked);


        Time.timeScale = 0;
        tapToPlayGameObject.SetActive(true);
    }

    private void OnPressToPlayButtonClicked()
    {
        Time.timeScale = 1.0f;
        tapToPlayGameObject.SetActive(false);
    }

    //public void StartLevel()
    //{
    //    Time.timeScale = 1.0f;
    //    tapToPlayGameObject.SetActive(false);
    //}

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayNextLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
    }

    public void ShowLevelFailedUI()
    {
        levelFailedUI.SetActive(true);
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
