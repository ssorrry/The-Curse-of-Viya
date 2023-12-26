using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "ScriptableObject/CharacterScriptableObject")]
public class CharacterScriptableObject : ScriptableObject
{
    public string nameCharacter;
    public Sprite characterSprite;
}
