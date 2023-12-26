using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject itemScrObj;
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public bool isEmpty = true;
    private Transform inventaryPanel;

    private void Start()
    {
        Transform parentTransform = transform.parent;
        inventaryPanel = parentTransform.parent;
    }
    public void SlotSelected() 
    {
        if (!isEmpty)
        {
            inventaryPanel.gameObject.SetActive(false);
            BlockKeys.InventoryClosed();
            itemScrObj.InteractWithItem();
        }
    }
}
