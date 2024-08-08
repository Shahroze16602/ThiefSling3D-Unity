using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManagement : MonoBehaviour
{
    // GameObjects accessed for display.
    public GameObject mainMenuPanel;
    public GameObject creditsMenuPanel;
    public GameObject levelsMenuPanel;

    // Menu UI.
    public Button playButton;
    public Button levelsButton;
    public Button levelsBackButton;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;
    public Button creditsButton;
    public Button creditsBackButton;
    //public Button levelsButton;

    // Levels UI.
    //public Button levelsMenuBackButton;
    //public Button level1Button;
    //public Button level2Button;



    public Text coinsText;
    public AudioSource buttonClickAudioSource;

    // Using it to generate coins using the help of button.
    public int totalCoinsCollected;

    String level1 = "Level1";
    String level2 = "Level2";
    String level3 = "Level3";
    String level4 = "Level4";
    String level5 = "Level5";



    private void Awake()
    {
        coinsText.text = "0";
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGameData();

        // Manually adding a button listner for better readability and understanding.
        playButton.onClick.AddListener(OnPlayButtonClicked);
        levelsButton.onClick.AddListener(OnLevelsButtonClicked);
        levelsBackButton.onClick.AddListener(OnLevelsMenuBackButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        creditsBackButton.onClick.AddListener(OnCreditsBackButtonClicked);

        level1Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(level1);
            buttonClickAudioSource.Play();
        });
        level2Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(level2);
            buttonClickAudioSource.Play();
        });
        level3Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(level3);
            buttonClickAudioSource.Play();
        });
        level4Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(level4);
            buttonClickAudioSource.Play();
        });
        level5Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(level5);
            buttonClickAudioSource.Play();
        });



        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
        creditsMenuPanel.SetActive(false);


    }

    private void OnCreditsBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        creditsMenuPanel.SetActive(false);
        buttonClickAudioSource.Play();
    }

    private void OnCreditsButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        creditsMenuPanel.SetActive(true);
        buttonClickAudioSource.Play();

    }


    // Update is called once per frame
    void Update()
    {
        //SaveGameData();


    }

    //public void SaveGameData()
    //{
    //    PlayerPrefs.SetInt("GoldCoins", totalCoinsCollected);
    //    PlayerPrefs.Save();
    //}
    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("CoinsCollected"))
        {
            totalCoinsCollected = PlayerPrefs.GetInt("CoinsCollected");
            coinsText.text = totalCoinsCollected.ToString();
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");

    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(level1);
        buttonClickAudioSource.Play();

    }





    #region Levels Menu UI
    private void OnLevelsButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        levelsMenuPanel.SetActive(true);
        buttonClickAudioSource.Play();
    }
    private void OnLevelsMenuBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
        buttonClickAudioSource.Play();
    }
    #endregion

}
