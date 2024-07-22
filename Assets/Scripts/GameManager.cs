using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject levelFailedUI;
    [SerializeField] private GameObject TapToPlayUi;

    private void Start()
    {
        Time.timeScale = 0;
        TapToPlayUi.SetActive(true);
    }

    public void StartLevel()
    {
        Time.timeScale = 1.0f;
        TapToPlayUi.SetActive(false);
    }

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
