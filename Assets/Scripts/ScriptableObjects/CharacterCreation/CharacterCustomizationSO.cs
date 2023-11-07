using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Customization", menuName ="Character Customization/Character")]
public class CharacterCustomizationSO : ScriptableObject
{
    public Sprite face;
    public List<Sprite> skin;
    public Sprite hair;
    public List<Sprite> shirt;
    public List<Sprite> pants;
    public Sprite shoes;
}
