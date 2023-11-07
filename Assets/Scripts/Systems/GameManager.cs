using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    #endregion

    #region Fields

    public PlayerController player;
    public bool isSelling;
    public bool gamePaused;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
    }

    #endregion
}
