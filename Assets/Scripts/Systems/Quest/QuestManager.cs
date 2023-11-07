using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<Quest> activeQuests = new List<Quest>();

    void Awake()
    {
        instance = this;
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        HUDManager.instance.activeQuest.gameObject.SetActive(true);
        HUDManager.instance.activeQuest.text = activeQuests[0].questName;
    }

    public void CompleteQuest(Quest quest)
    {
        if (Inventory.instance.items.Contains(quest.requiredItem))
        {
            Debug.Log("Quest completed");
            GameManager.instance.player.totalMoney += quest.reward;
            activeQuests.Remove(quest);
            Inventory.instance.Remove(quest.requiredItem);
        }

        else
        {
            Debug.Log("Quest cannot be completed. Required item not found.");
        }
    }
}
