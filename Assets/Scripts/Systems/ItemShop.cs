using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HAIR,
    SHIRT,
    PANTS,
    SHOES,
    QUEST_ITEM
}


public class ItemShop : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void BuyItem(Item item)
    {
        if (GameManager.instance.player.totalMoney >= item.itemCost)
        {
            if (Inventory.instance.space > Inventory.instance.items.Count)
            {
                GameManager.instance.player.totalMoney -= item.itemCost;
                Inventory.instance.Add(item);
                HUDManager.instance.sellButton.interactable = true;
            }

            else
            {
                Debug.Log("Inventory full");
            }
        }
        else
        {
            Debug.Log("Not enough money to buy " + item.name);
        }

    }
}
