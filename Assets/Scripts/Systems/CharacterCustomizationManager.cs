using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationManager : MonoBehaviour
{
    [SerializeField] private CharacterCustomizationSO _characterAssets;

    [SerializeField] private Sprite _faceSprite;
    [SerializeField] private List<Sprite> _skinSprite;
    [SerializeField] private Sprite _hairSprite;
    [SerializeField] private List<Sprite> _shirtSprite;
    [SerializeField] private List<Sprite> _pantsSprite;
    [SerializeField] private Sprite _shoesSprite;

    public void SetFace()
    {
        _characterAssets.face = _faceSprite;
        Menu.instance.faceSprite.GetComponent<SpriteRenderer>().sprite = _faceSprite;
    }

    public void SetHair()
    {
        _characterAssets.hair = _hairSprite;
        Menu.instance.hairSprite.GetComponent<SpriteRenderer>().sprite = _hairSprite;
    }

    public void SetShoe()
    {
        _characterAssets.shoes = _shoesSprite;

        for (int i = 0; i < Menu.instance.shoesSprite.Count; i++)
        {
            Menu.instance.shoesSprite[i].GetComponent<SpriteRenderer>().sprite = _shoesSprite;
        }
    }

    public void SetSkin()
    {
        for (int i = 0; i < _skinSprite.Count; i++)
        {
            _characterAssets.skin[i] = _skinSprite[i];
            Menu.instance.skinSprite[i].GetComponent<SpriteRenderer>().sprite = _skinSprite[i];
        }
    }

    public void SetShirt()
    {
        for (int i = 0; i < _shirtSprite.Count; i++)
        {
            _characterAssets.shirt[i] = _shirtSprite[i];
            Menu.instance.shirtSprite[i].GetComponent<SpriteRenderer>().sprite = _shirtSprite[i];
        }
    }

    public void SetPants()
    {
        for (int i = 0; i < _pantsSprite.Count; i++)
        {
            _characterAssets.pants[i] = _pantsSprite[i];
            Menu.instance.pantsSprite[i].GetComponent<SpriteRenderer>().sprite = _pantsSprite[i];

        }
    }
}
