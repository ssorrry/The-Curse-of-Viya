using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private Transform inventorySlotPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private void Awake()
    {
        inventoryPanel.SetActive(false);
        for (int i = 0; i < inventorySlotPanel.childCount; i++) 
        {
            if (inventorySlotPanel.GetChild(i).GetComponent<InventorySlot>() != null) 
            {
                slots.Add(inventorySlotPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        foreach (InventorySlot slot in slots) 
        {
            if (slot.isEmpty) 
            {
                slot.itemName.color = new Color(1f, 1f, 1f, 0f);
                slot.itemIcon.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !BlockKeys.dialogOpen) 
        {
            if (inventoryPanel.activeSelf == false)
            {
                //GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
                inventoryPanel.SetActive(true);
                slots[0].gameObject.GetComponent<Button>().Select();
                BlockKeys.InventoryOpened();
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Speed", 0f);
                float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
                float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
                if (vertical != 0 && horizontal != 0)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
                }
            }
            else 
            {
                inventoryPanel.SetActive(false);
                BlockKeys.InventoryClosed();
            }
        }
    }
    public void AddItem(ItemScriptableObject itemScrObj) 
    { 
        foreach (InventorySlot slot in slots) 
        {
            if (slot.isEmpty == true)
            {
                slot.itemScrObj = itemScrObj;
                slot.itemName.text = itemScrObj.itemName;
                slot.itemDescription.text = itemScrObj.itemDescription;
                slot.itemIcon.sprite = itemScrObj.itemIcon;
                slot.isEmpty = false;
                slot.itemName.color = new Color(1f, 1f, 1f, 1f);
                slot.itemIcon.color = new Color(1f, 1f, 1f, 1f);
                break;
            }
        }
    }

    public void RemoveItem(ItemScriptableObject itemScrObj)
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.isEmpty && slot.itemScrObj == itemScrObj)
            {
                slot.itemScrObj = null;
                slot.itemName.text = "";
                slot.itemDescription.text = "";
                slot.itemIcon.sprite = null;
                slot.isEmpty = true;

                ShiftItems();
                break;
            }
        }
    }
    private void ShiftItems()
    {
        for (int i = 0; i < slots.Count - 1; i++)
        {
            if (slots[i].isEmpty && !slots[i + 1].isEmpty)
            {
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>($"Item/{slots[i+1].itemScrObj.itemName}");
                slots[i].itemScrObj = item;
                slots[i].itemName.text = item.itemName;
                slots[i].itemDescription.text = item.itemDescription;
                slots[i].itemIcon.sprite = item.itemIcon;
                slots[i].isEmpty = false;
                slots[i].itemName.color = new Color(1f, 1f, 1f, 1f);
                slots[i].itemIcon.color = new Color(1f, 1f, 1f, 1f);

                slots[i + 1].itemScrObj = null;
                slots[i + 1].itemName.text = "";
                slots[i + 1].itemDescription.text = "";
                slots[i + 1].itemIcon.sprite = null;
                slots[i + 1].isEmpty = true;
                slots[i + 1].itemName.color = new Color(1f, 1f, 1f, 0f);
                slots[i + 1].itemIcon.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}
