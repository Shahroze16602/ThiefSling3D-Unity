using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManagement : MonoBehaviour
{
    // GameObjects accessed for display.
    public GameObject titleScreenPanel;
    public GameObject mainMenuPanel;
    public GameObject levelsMenuPanel;
    public GameObject upgradesMenuPanel;

    // Title Screen UI
    public Button titleContinueButton;


    // Menu UI.
    public Button playButton;
    public Button levelsButton;
    public Button upgradesButton;
    public Button optionsButton;

    // Levels UI.
    public Button levelsMenuBackButton;
    public Button level1Button;
    public Button level2Button;

    // Upgrades Menu UI.
    public Button upgradesMenuBackButton;
    public Button upgrade1Button;
    public Button upgrade2Button;
    public Button upgrade3Button;
    public Button upgrade4Button;

    public Text coinsText;
    public AudioSource buttonClickAudioSource;

    // Using it to generate coins using the help of button.
    public int totalCoinsCollected;


    private void Awake()
    {
        coinsText.text = "0";
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGameData();

        // Manually adding a button listner for better readability and understanding.
        titleContinueButton.onClick.AddListener(OnTitleContinueButtonClicked);
        playButton.onClick.AddListener(OnPlayButtonClicked);
        levelsButton.onClick.AddListener(OnLevelsButtonClicked);
        upgradesButton.onClick.AddListener(OnUpgradesButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);

        levelsMenuBackButton.onClick.AddListener(OnLevelsMenuBackButtonClicked);
        level1Button.onClick.AddListener(OnLevel1ButtonClicked);
        level2Button.onClick.AddListener(OnLevel2ButtonClicked);

        upgradesMenuBackButton.onClick.AddListener(OnUpgradesMenuBackButtonClicked);
        upgrade1Button.onClick.AddListener(OnUpgrades1ButtonClicked);
        upgrade2Button.onClick.AddListener(OnUpgrades2ButtonClicked);
        upgrade3Button.onClick.AddListener(OnUpgrades3ButtonClicked);
        upgrade4Button.onClick.AddListener(OnUpgrades4ButtonClicked);

        titleScreenPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        levelsMenuPanel.SetActive(false);
        upgradesMenuPanel.SetActive(false);
    }

    private void OnTitleContinueButtonClicked()
    {
        titleScreenPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        buttonClickAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //SaveGameData();

        if (totalCoinsCollected >= 10)
            upgrade1Button.interactable = true;
        if (totalCoinsCollected >= 20)
            upgrade2Button.interactable = true;
        if (totalCoinsCollected >= 30)
            upgrade3Button.interactable = true;
        if (totalCoinsCollected >= 40)
            upgrade4Button.interactable = true;
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

    #region Menu UI
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");
        buttonClickAudioSource.Play();

    }

    private void OnLevelsButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        levelsMenuPanel.SetActive(true); 
        buttonClickAudioSource.Play();

    }

    private void OnUpgradesButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        upgradesMenuPanel.SetActive(true); 
        buttonClickAudioSource.Play();
    }

    private void OnOptionsButtonClicked()
    {
        buttonClickAudioSource.Play();
        throw new NotImplementedException(); 
    }
    #endregion

    #region Levels Menu UI
    private void OnLevelsMenuBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
        buttonClickAudioSource.Play();
    }
    private void OnLevel1ButtonClicked()
    {
        SceneManager.LoadScene("Level1");
        buttonClickAudioSource.Play();
    }
    private void OnLevel2ButtonClicked()
    {
        SceneManager.LoadScene("Level2");
        buttonClickAudioSource.Play();
    }
    #endregion

    #region Upgrades Menu UI
    private void OnUpgradesMenuBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        upgradesMenuPanel.SetActive(false);
        buttonClickAudioSource.Play();
    }

    private void OnUpgrades1ButtonClicked()
    {
        Debug.Log("Upgrade 1 Equipped");
        buttonClickAudioSource.Play();
    }
    private void OnUpgrades4ButtonClicked()
    {
        Debug.Log("Upgrade 2 Equipped");
        buttonClickAudioSource.Play();
    }

    private void OnUpgrades3ButtonClicked()
    {
        Debug.Log("Upgrade 3 Equipped");
        buttonClickAudioSource.Play();
    }

    private void OnUpgrades2ButtonClicked()
    {
        Debug.Log("Upgrade 4 Equipped");
        buttonClickAudioSource.Play();
    }
    #endregion




 
}
