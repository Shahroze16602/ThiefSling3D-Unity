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


    // UI Elements.
    public Button playButton;
    public Button levelsButton;
    public Button upgradesButton;
    public Button optionsButton;

    public Button levelsMenuBackButton;
    public Button coinsButton;

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
        coinsButton.onClick.AddListener(OnCoinsMenuBackButtonClicked);

        coinsButtonText.text = string.Empty;

        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
    }

    private void OnCoinsMenuBackButtonClicked()
    {
        // Adds gold coin on every button click.
        goldCoins += goldCoinPerClick;
        coinsButtonText.text = goldCoins.ToString();
    }

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
        throw new NotImplementedException();
    }

    private void OnOptionsButtonClicked()
    {
        throw new NotImplementedException();
    }


    private void OnLevelsMenuBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
