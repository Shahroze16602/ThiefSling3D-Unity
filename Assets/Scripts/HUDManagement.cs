using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManagement : MonoBehaviour
{
    // GameObjects accessed for display.
    public GameObject mainMenuPanel;
    public GameObject levelsMenuPanel;
    public GameObject upgradesMenuPanel;


    // Menu UI.
    public Button playButton;
    public Button levelsButton;
    public Button upgradesButton;
    public Button optionsButton;

    
    public Button coinsButton;

    // Levels UI.
    public Button levelsMenuBackButton;
    public Button level1Button;

    // Upgrades Menu UI.
    public Button upgradesMenuBackButton;
    public Button upgrade1Button;
    public Button upgrade2Button;
    public Button upgrade3Button;
    public Button upgrade4Button;

    public Text coinsButtonText;

    // Using it to generate coins using the help of button.
    public int goldCoins;
    public int goldCoinPerClick = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Manually adding a button listner for better readability and understanding.
        playButton.onClick.AddListener(OnPlayButtonClicked);
        levelsButton.onClick.AddListener(OnLevelsButtonClicked);
        upgradesButton.onClick.AddListener(OnUpgradesButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);

        levelsMenuBackButton.onClick.AddListener(OnLevelsMenuBackButtonClicked);
        level1Button.onClick.AddListener(OnLevel1ButtonClicked);

        coinsButton.onClick.AddListener(OnCoinsButtonClicked);

        upgradesMenuBackButton.onClick.AddListener(OnUpgradesMenuBackButtonClicked);
        upgrade1Button.onClick.AddListener(OnUpgrades1ButtonClicked);
        upgrade2Button.onClick.AddListener(OnUpgrades2ButtonClicked);
        upgrade3Button.onClick.AddListener(OnUpgrades3ButtonClicked);
        upgrade4Button.onClick.AddListener(OnUpgrades4ButtonClicked);

        coinsButtonText.text = string.Empty;

        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
        upgradesMenuPanel.SetActive(false);
    }

  

    

    private void OnCoinsButtonClicked()
    {
        // Adds gold coin on every button click.
        goldCoins += goldCoinPerClick;
        coinsButtonText.text = goldCoins.ToString();
    }

    #region Menu UI
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");

    }

    private void OnLevelsButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        levelsMenuPanel.SetActive(true);

    }

    private void OnUpgradesButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        upgradesMenuPanel.SetActive(true);
    }

    private void OnOptionsButtonClicked()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Levels Menu UI
    private void OnLevelsMenuBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
    }
    private void OnLevel1ButtonClicked()
    {
        SceneManager.LoadScene("Level1");
    }
    #endregion

    #region Upgrades Menu UI
    private void OnUpgradesMenuBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        upgradesMenuPanel.SetActive(false);
    }

    private void OnUpgrades1ButtonClicked()
    {
        Debug.Log("Upgrade 1 Equipped");
    }
    private void OnUpgrades4ButtonClicked()
    {
        Debug.Log("Upgrade 2 Equipped"); ;
    }

    private void OnUpgrades3ButtonClicked()
    {
        Debug.Log("Upgrade 3 Equipped"); ;
    }

    private void OnUpgrades2ButtonClicked()
    {
        Debug.Log("Upgrade 4 Equipped"); ;
    }
    #endregion




    // Update is called once per frame
    void Update()
    {
        if (goldCoins >= 10)
            upgrade1Button.interactable = true;
        if (goldCoins >= 20)
            upgrade2Button.interactable = true;
        if (goldCoins >= 30)
            upgrade3Button.interactable = true;
        if (goldCoins >= 40)
            upgrade4Button.interactable = true;

    }
}
