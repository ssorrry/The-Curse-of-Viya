using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController: MonoBehaviour
{
    protected GameObject player;
    protected List<string> itemNames = new List<string>();
    [SerializeField] protected InventoryManager inventoryManager;
    public static string nameFile;

    protected virtual void Awake()
    {
        Debug.Log(nameFile);
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (nameFile != null)
        {
            LoadProgressScene(nameFile);
        }
    }

    protected virtual void Update() { }
    protected virtual void SaveProgressScene() { }

    protected virtual void LoadProgressScene(string nameFile)
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            itemNames = loadedProgress.itemNames;
            LoadInventory(itemNames);
        }
    }

    protected virtual void LoadInventory(List<string> array)
    {
        if (array != null)
        {
            ClearInventory();
            for (int j = 0; j < array.Count; j++)
            {
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>($"Item/{array[j]}");
                for (int i = 0; i < inventoryManager.slots.Count; i++)
                {
                    if (inventoryManager.slots[i].isEmpty)
                    {
                        inventoryManager.slots[i].itemScrObj = item;
                        inventoryManager.slots[i].itemName.text = item.itemName;
                        inventoryManager.slots[i].itemDescription.text = item.itemDescription;
                        inventoryManager.slots[i].itemIcon.sprite = item.itemIcon;
                        inventoryManager.slots[i].isEmpty = false;
                        break;
                    }
                }
            }
        }

        Debug.Log("Load inventory");
    }
    protected virtual void ClearInventory()
    {
        for (int i = 0; i < inventoryManager.slots.Count; i++)
        {
            inventoryManager.slots[i].itemScrObj = null;
            inventoryManager.slots[i].itemName.text = "";
            inventoryManager.slots[i].itemDescription.text = "";
            inventoryManager.slots[i].itemIcon.sprite = null;
            inventoryManager.slots[i].isEmpty = true;
        }
    }

}
