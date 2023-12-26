using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/SheetsScriptableObject")]
public class SheetsScriptableObject : ItemScriptableObject
{
    public GameObject sheetPrefab;
    public override void InteractWithItem()
    {
        base.InteractWithItem();
        BlockKeys.DialogOpened();
        Instantiate(sheetPrefab, GameObject.FindWithTag("Canvas").transform);
    }
}
