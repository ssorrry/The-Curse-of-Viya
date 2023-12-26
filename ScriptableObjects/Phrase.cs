using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Phrase
{
    public CharacterScriptableObject characterScriptableObject;
    public string textPhrase;

    public Phrase(CharacterScriptableObject characterScriptableObject, string textPhrase)
    {
        this.characterScriptableObject = characterScriptableObject;
        this.textPhrase = textPhrase;
    }
}
