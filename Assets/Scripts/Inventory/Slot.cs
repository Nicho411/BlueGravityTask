using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _costText;

    void Start()
    {
        _sprite.sprite = _item.itemSprite[0];
        _costText.text = _item.itemCost.ToString();
    }

    public void MouseOver()
    {
        if (_item != null)
        {
            HUDManager.instance.itemNameText.gameObject.SetActive(true);
            HUDManager.instance.itemDescriptionText.gameObject.SetActive(true);

            HUDManager.instance.itemNameText.text = _item.itemName;
            HUDManager.instance.itemDescriptionText.text = _item.itemDescription;
        }
    }

    public void MouseQuit()
    {
        HUDManager.instance.itemNameText.gameObject.SetActive(false);
        HUDManager.instance.itemDescriptionText.gameObject.SetActive(false);

        HUDManager.instance.itemNameText.text = "";
        HUDManager.instance.itemDescriptionText.text = "";
    }

}
