using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    QUEST_GIVER,
    VENDOR
}

public class NPC : MonoBehaviour
{
    #region Fields

    public NPCType npcType;

    [SerializeField] private string[] _dialogueLines;
    private int _currentLine;

    [SerializeField] private Quest quest;
    [SerializeField] private string[] _questCompleteDialogue;

    [SerializeField] private GameObject _itemToSpawn;
    [SerializeField] private List<Transform> _itemSpawnPoints;

    public bool questCompleted;

    #endregion

    #region Public Methods

    public void DisplayLine()
    {
        HUDManager.instance.dialogueText.text = _dialogueLines[_currentLine];
    }

    public void DisplayQuestCompleteLine()
    {
        HUDManager.instance.dialogueText.text = _questCompleteDialogue[_currentLine];
    }

    public void ContinueDialogue()
    {
        _currentLine++;

        if (_currentLine < _dialogueLines.Length)
        {
            DisplayLine();
        }

        else
        {
            HUDManager.instance.dialogueText.text = "";
            HUDManager.instance.dialoguePanel.SetActive(false);
            GameManager.instance.player.isDialogueOpen = false;
            HUDManager.instance.continueDialogueButton.onClick.RemoveAllListeners();
            _currentLine = 0;

            switch (npcType)
            {
                case NPCType.QUEST_GIVER:
                    if (quest != null)
                    {
                        QuestManager.instance.AddQuest(quest);
                        SpawnQuestItem();
                    }
                    break;

                case NPCType.VENDOR:
                    HUDManager.instance.shopPanel.SetActive(true);
                    GameManager.instance.player.storeOpen = true;

                    if (Inventory.instance.items.Count > 0)
                    {
                        HUDManager.instance.sellButton.interactable = true;
                    }
                    else
                    {
                        HUDManager.instance.sellButton.interactable = false;
                    }

                    break;
            }

        }
    }

    public void QuestCompleted()
    {
        _currentLine++;

        if (_currentLine < _questCompleteDialogue.Length)
        {
            DisplayQuestCompleteLine();
        }

        else
        {
            HUDManager.instance.dialogueText.text = "";
            HUDManager.instance.dialoguePanel.SetActive(false);
            HUDManager.instance.continueDialogueButton.onClick.RemoveAllListeners();
            GameManager.instance.player.isDialogueOpen = false;
            questCompleted = true;

            //Inventory.instance.Remove(quest.requiredItem);

            for (int i = 0; i < Inventory.instance.slots.Count; i++)
            {
                InventorySlot currentSlot = Inventory.instance.slots[i];

                if (currentSlot.item == quest.requiredItem)
                {
                    GameObject newSlot = Instantiate(Inventory.instance.inventorySlotPrefab, currentSlot.transform.parent);

                    Inventory.instance.slots.Remove(Inventory.instance.slots[i]);
                    Inventory.instance.slots.Add(newSlot.GetComponent<InventorySlot>());
                    Destroy(currentSlot.transform.GetChild(0).gameObject);
                    Destroy(currentSlot.gameObject);
                }
            }

            _currentLine = 0;
            QuestManager.instance.CompleteQuest(quest);
            quest = null;
        }
    }

    #endregion

    #region Private Methods

    private void SpawnQuestItem()
    {
        Transform spawnLocation = _itemSpawnPoints[Random.Range(0, _itemSpawnPoints.Count)];
        Instantiate(_itemToSpawn, spawnLocation.position, Quaternion.identity);
    }

    #endregion
}
