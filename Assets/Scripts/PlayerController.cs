using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    #region Cached Components

    private Rigidbody2D _rb;
    private Animator _anim;

    #endregion

    #region Fields

    [BoxGroup("Character Style")]
    public CharacterCustomizationSO characterAssets;
    [BoxGroup("Character Style")]
    [SerializeField] private GameObject _faceSprite;
    [BoxGroup("Character Style")]
    [SerializeField] private List<GameObject> _skinSprite;
    [BoxGroup("Character Style")]
    [SerializeField] private GameObject _hairSprite;
    [BoxGroup("Character Style")]
    [SerializeField] private List<GameObject> _shirtSprite;
    [BoxGroup("Character Style")]
    [SerializeField] private List<GameObject> _pantsSprite;
    [BoxGroup("Character Style")]
    [SerializeField] private List<GameObject> _shoesSprite;

    private float _horizontal;
    private float _vertical;
    private Vector2 _moveAmount;
    [BoxGroup("Player settings")]
    [SerializeField] private float _moveSpeed;
    [BoxGroup("Player settings")]
    [SerializeField] private int _totalMoney;
    public int totalMoney
    {
        get => _totalMoney;
        set
        {
            _totalMoney = value;
            HUDManager.instance.moneyText.text = value.ToString();
        }
    }

    public bool inventoryOpen;
    public bool isDialogueOpen;
    public bool storeOpen;
    private bool _canInteract;


    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HUDManager.instance.moneyText.text = _totalMoney.ToString();
        InitializeCharacter();   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
    }

    private void FixedUpdate()
    {
        if (!inventoryOpen && !isDialogueOpen && !storeOpen)
        {
            _rb.MovePosition(_rb.position + _moveAmount * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _canInteract = true;

            if (other.GetComponent<NPC>().npcType == NPCType.QUEST_GIVER)
            {
                if (QuestManager.instance.activeQuests.Count <= 0 && !other.GetComponent<NPC>().questCompleted)
                {
                    other.GetComponent<NPC>().DisplayLine();
                    HUDManager.instance.interactText.SetActive(true);
                    HUDManager.instance.continueDialogueButton.onClick.AddListener(other.GetComponent<NPC>().ContinueDialogue);
                }

                else if (QuestManager.instance.activeQuests.Count > 0 && Inventory.instance.items.Contains(QuestManager.instance.activeQuests[0].requiredItem))
                {
                    other.GetComponent<NPC>().DisplayQuestCompleteLine();
                    HUDManager.instance.interactText.SetActive(true);
                    HUDManager.instance.continueDialogueButton.onClick.AddListener(other.GetComponent<NPC>().QuestCompleted);
                }
            }

            if (other.GetComponent<NPC>().npcType == NPCType.VENDOR) 
            {
                other.GetComponent<NPC>().DisplayLine();
                HUDManager.instance.interactText.SetActive(true);
                HUDManager.instance.continueDialogueButton.onClick.AddListener(other.GetComponent<NPC>().ContinueDialogue);
            }
        }

        if (other.CompareTag("Teleport"))
        {
            other.GetComponent<FastTravel>().Teleport(this.gameObject);
            StartCoroutine(FadeCor());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _canInteract = false;
            isDialogueOpen = false;
            HUDManager.instance.interactText.SetActive(false);
        }
    }

    #endregion

    #region Private Methods

    private void PlayerInputs()
    {
        if (!inventoryOpen && !isDialogueOpen && !storeOpen)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");

            Vector2 moveInput = new Vector2(_horizontal, _vertical);

            _moveAmount = moveInput.normalized * _moveSpeed;
            _anim.SetBool("isWalking", moveInput != Vector2.zero);
        }

        if (Input.GetKeyDown(KeyCode.I) && !GameManager.instance.gamePaused)
        {
            HUDManager.instance.inventoryPanel.SetActive(!HUDManager.instance.inventoryPanel.activeSelf);
            inventoryOpen = !inventoryOpen;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.instance.gamePaused)
            {
                Time.timeScale = 0;
                GameManager.instance.gamePaused = true;
                HUDManager.instance.pausePanel.SetActive(true);
            }

            else
            {
                Time.timeScale = 1;
                GameManager.instance.gamePaused = false;
                HUDManager.instance.pausePanel.SetActive(false);
            }
        }

        if (_canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HUDManager.instance.dialoguePanel.SetActive(true);
                HUDManager.instance.interactText.SetActive(false);
                isDialogueOpen = true;
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Set the character style
    /// </summary>

    public void InitializeCharacter()
    {
        _faceSprite.GetComponent<SpriteRenderer>().sprite = characterAssets.face;
        _hairSprite.GetComponent<SpriteRenderer>().sprite = characterAssets.hair;

        for (int i = 0; i < _shoesSprite.Count; i++)
        {
            _shoesSprite[i].GetComponent<SpriteRenderer>().sprite = characterAssets.shoes;
        }

        for (int i = 0; i < characterAssets.skin.Count; i++)
        {
            _skinSprite[i].GetComponent<SpriteRenderer>().sprite = characterAssets.skin[i];
        }

        for (int i = 0; i < characterAssets.shirt.Count; i++) 
        {
            _shirtSprite[i].GetComponent<SpriteRenderer>().sprite = characterAssets.shirt[i];
        }

        for (int i = 0; i < characterAssets.pants.Count; i++)
        {
            _pantsSprite[i].GetComponent<SpriteRenderer>().sprite = characterAssets.pants[i];
        }        
    }

    #endregion

    #region Coroutines

    private IEnumerator FadeCor()
    {
        HUDManager.instance.teleportFade.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        HUDManager.instance.teleportFade.SetActive(false);
    }

    #endregion
}
