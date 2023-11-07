using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public List<Sprite> itemSprite;
    public int itemCost;
    public int sellPrice;
}
