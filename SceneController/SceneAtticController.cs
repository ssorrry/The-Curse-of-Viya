using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAtticController : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private InventoryManager inventoryManager;
    private string nameFile = "/currentSession.dat";
    private List<string> itemNames = new List<string>();
    public static bool chase;
    [SerializeField]
    private GameObject tabouret;
    [SerializeField]
    private Transform tabouretPosition;
    [SerializeField]
    private GameObject wardrobe;
    [SerializeField]
    private Transform wardrobePosition;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = true;
        LoadProgressScene();
    }
    private void Update()
    {
        if (chase) 
        {
            chase = false;
            
            StartCoroutine(Tabouret());
            StartCoroutine(Wardrobe());
        }
    }
    void LoadProgressScene()
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
            for (int j = 0; j < array.Count; j++)
            {
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>($"Item/{array[j]}");
                Debug.Log(inventoryManager.slots.Count);
                for (int i = 0; i < inventoryManager.slots.Count; i++)
                {
                    if (inventoryManager.slots[i].isEmpty)
                    {
                        inventoryManager.slots[i].itemScrObj = item;
                        inventoryManager.slots[i].itemName.text = item.itemName;
                        inventoryManager.slots[i].itemDescription.text = item.itemDescription;
                        inventoryManager.slots[i].itemIcon.sprite = item.itemIcon;
                        inventoryManager.slots[i].isEmpty = false;
                        inventoryManager.slots[i].itemName.color = new Color(1f, 1f, 1f, 1f);
                        inventoryManager.slots[i].itemIcon.color = new Color(1f, 1f, 1f, 1f);
                        Debug.Log("if (inventoryManager.slots[i].isEmpty)");
                        break;

                    }
                }
            }
        }
    }

    IEnumerator Wardrobe() 
    {
        yield return new WaitForSeconds(1f);
        wardrobe.transform.position = wardrobePosition.position;
    }
    IEnumerator Tabouret()
    {
        yield return new WaitForSeconds(0.7f);
        tabouret.transform.position = tabouretPosition.position;
    }
}
