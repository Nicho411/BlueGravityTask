using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    #region Singleton

    public static HUDManager instance;

    #endregion

    #region Fields

    [BoxGroup("Menu")]
    public GameObject characterCustomizationPanel;
    [BoxGroup("Menu")]
    public GameObject startButton;
    [BoxGroup("Menu")]
    public GameObject quitButton;

    [BoxGroup("Game canvas")]
    public TextMeshProUGUI moneyText;
    [BoxGroup("Game canvas")]
    public TextMeshProUGUI activeQuest;
    [BoxGroup("Game canvas")]
    public GameObject interactText;
    [BoxGroup("Game canvas")]
    public GameObject pausePanel;

    [BoxGroup("Inventory")]
    public GameObject inventoryPanel;

    [BoxGroup("Shop")]
    public GameObject shopPanel;
    [BoxGroup("Shop")]
    public TextMeshProUGUI itemNameText;
    [BoxGroup("Shop")]
    public TextMeshProUGUI itemDescriptionText;
    [BoxGroup("Shop")]
    public Button sellButton;

    [BoxGroup("Dialogue")]
    public GameObject dialoguePanel;
    [BoxGroup("Dialogue")]
    public TextMeshProUGUI dialogueText;
    [BoxGroup("Dialogue")]
    public Button continueDialogueButton;

    public GameObject teleportFade;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        GameManager.instance.player.inventoryOpen = false;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        GameManager.instance.isSelling = false;
        GameManager.instance.player.storeOpen = false;

        shopPanel.transform.GetChild(0).gameObject.SetActive(true);
        inventoryPanel.transform.GetChild(0).gameObject.SetActive(true);
        inventoryPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        shopPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void OnSellClick()
    {
        inventoryPanel.SetActive(true);
        GameManager.instance.isSelling = true;

        shopPanel.transform.GetChild(0).gameObject.SetActive(false);
        inventoryPanel.transform.GetChild(0).gameObject.SetActive(false);

        inventoryPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(478, 0, 0);
        shopPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(-478, 0, 0);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Unpause()
    {
        GameManager.instance.gamePaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
