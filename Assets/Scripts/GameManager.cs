using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tapToPlayGameObject;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject levelFailedUI;
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioSource levelEndAudioSource;
    [SerializeField] private AudioClip levelFailedAudioClip;
    [SerializeField] private AudioClip levelCompletedAudioClip;

    private void Start()
    {
        Time.timeScale = 0;
        tapToPlayGameObject.SetActive(true);
    }

    public void StartLevel()
    {
        Time.timeScale = 1.0f;
        buttonClickAudioSource.Play();
        tapToPlayGameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        buttonClickAudioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayNextLevel()
    {
        Time.timeScale = 1.0f;
        buttonClickAudioSource.Play();
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
        levelEndAudioSource.clip = levelCompletedAudioClip;
        levelEndAudioSource.Play();
    }

    public void ShowLevelFailedUI()
    {
        levelFailedUI.SetActive(true);
        levelEndAudioSource.clip = levelFailedAudioClip;
        levelEndAudioSource.Play();
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        buttonClickAudioSource.Play();
        SceneManager.LoadScene(0);
    }
}
