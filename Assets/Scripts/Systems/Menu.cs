using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    [SerializeField] private CharacterCustomizationSO _characterAssets;

    public GameObject faceSprite;
    public List<GameObject> skinSprite;
    public GameObject hairSprite;
    public List<GameObject> shirtSprite;
    public List<GameObject> pantsSprite;
    public List<GameObject> shoesSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeCharacter();
    }

    public void StartGame()
    {
        HUDManager.instance.characterCustomizationPanel.SetActive(true);
        HUDManager.instance.startButton.SetActive(false);
        HUDManager.instance.quitButton.SetActive(false);
    }

    public void Ready()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR

        EditorApplication.isPlaying = false;

#endif

    }

    public void InitializeCharacter()
    {
        faceSprite.GetComponent<SpriteRenderer>().sprite = _characterAssets.face;
        hairSprite.GetComponent<SpriteRenderer>().sprite = _characterAssets.hair;

        for (int i = 0; i < shoesSprite.Count; i++)
        {
            shoesSprite[i].GetComponent<SpriteRenderer>().sprite = _characterAssets.shoes;
        }

        for (int i = 0; i < _characterAssets.skin.Count; i++)
        {
            skinSprite[i].GetComponent<SpriteRenderer>().sprite = _characterAssets.skin[i];
        }

        for (int i = 0; i < _characterAssets.shirt.Count; i++)
        {
            shirtSprite[i].GetComponent<SpriteRenderer>().sprite = _characterAssets.shirt[i];
        }

        for (int i = 0; i < _characterAssets.pants.Count; i++)
        {
            pantsSprite[i].GetComponent<SpriteRenderer>().sprite = _characterAssets.pants[i];
        }
    }
}
