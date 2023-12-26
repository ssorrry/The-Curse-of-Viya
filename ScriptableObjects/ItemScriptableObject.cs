using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject")]
public class ItemScriptableObject: ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;   
    public Sprite itemIcon;
    public string itemDescription;

    public virtual void InteractWithItem()
    {
        Debug.Log("Interacting with item");
    }
}
