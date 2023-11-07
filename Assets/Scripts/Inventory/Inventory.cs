using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    #endregion

    #region Fields

    public List<Item> items = new List<Item>();
    public int space = 12;

    public List<InventorySlot> slots;

    public GameObject inventoryItemPrefab;
    public GameObject inventorySlotPrefab;

    #endregion

    #region MonoBehaviour Methods

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        space = slots.Count;
    }

    #endregion

    #region Private Methods

    #endregion

    #region Public Methods

    public void Add(Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot tempSlot = slots[i];
            InventoryItem itemInSlot = slots[i].GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                items.Add(item);
                SpawnItem(item, tempSlot);
                tempSlot.AddItem(item);
                return;
            }
        }
    }

    public void SpawnItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        newItem.transform.SetAsFirstSibling();
        InventoryItem invItem = newItem.GetComponentInChildren<InventoryItem>();

        invItem.InitializeItem(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.HAIR:
                GameManager.instance.player.characterAssets.hair = item.itemSprite[0];
                break;

            case ItemType.SHIRT:
                for (int i = 0; i < GameManager.instance.player.characterAssets.shirt.Count; i++)
                {
                    GameManager.instance.player.characterAssets.shirt[i] = item.itemSprite[i];
                }

                break;

            case ItemType.PANTS:
                for (int i = 0; i < GameManager.instance.player.characterAssets.pants.Count; i++)
                {
                    GameManager.instance.player.characterAssets.pants[i] = item.itemSprite[i];
                }

                break;

            case ItemType.SHOES:
                for (int i = 0; i < GameManager.instance.player.characterAssets.pants.Count; i++)
                {
                    GameManager.instance.player.characterAssets.pants[i] = item.itemSprite[0];
                }

                break;
        }

        GameManager.instance.player.InitializeCharacter();
    }

    #endregion
}