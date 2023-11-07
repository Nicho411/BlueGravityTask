using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item item;
    public Image itemImage;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.itemSprite[0];
    }

}
