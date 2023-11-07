using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;

    [SerializeField] private GameObject _mouseOverPanel;

    private void Start()
    {
        
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void ClearSlot()
    {
        item = null;
    }

    public void RemoveItem()
    {
        Inventory.instance.Remove(item);
        ClearSlot();

        GameObject newSlot = Instantiate(Inventory.instance.inventorySlotPrefab, this.transform.parent);

        Inventory.instance.slots.Remove(this);
        Inventory.instance.slots.Add(newSlot.GetComponent<InventorySlot>());
        Destroy(transform.GetChild(0).gameObject);
        Destroy(gameObject);
    }

    public void SellItem()
    {
        GameManager.instance.player.totalMoney += item.sellPrice;
        Inventory.instance.Remove(item);
        //ClearSlot();

        GameObject newSlot = Instantiate(Inventory.instance.inventorySlotPrefab, this.transform.parent);

        Inventory.instance.slots.Remove(this);
        Inventory.instance.slots.Add(newSlot.GetComponent<InventorySlot>());
        Destroy(transform.GetChild(0).gameObject);
        Destroy(gameObject);
    }

    public void MouseOver()
    {
        if (item != null)
        {
            if (item.itemType == ItemType.QUEST_ITEM)
            {
                return;
            }

            _mouseOverPanel.SetActive(true);

            if (GameManager.instance.isSelling)
            {
                _mouseOverPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                _mouseOverPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            }

        }
    }

    public void MouseQuit()
    {
        _mouseOverPanel.SetActive(false);
        _mouseOverPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
    }

    public void UseItem()
    {
        Inventory.instance.UseItem(item);
    }
}

