using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLadderController: MonoBehaviour
{
    private GameObject player;
    public static bool transitionFromAttic;
    [SerializeField]
    private Transform positionPlayer;
    private string nameFile = "/currentSession.dat";
    private List<string> itemNames = new List<string>();
    [SerializeField]
    private InventoryManager inventoryManager;
    private bool sceneAtticEnemy;
    [SerializeField]
    private GameObject enemyPrefab;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = true;
        if (transitionFromAttic)
        {
            transitionFromAttic = false;
            player.transform.position = new Vector3(positionPlayer.position.x, positionPlayer.position.y, positionPlayer.position.z);
        }
        LoadProgressScene();
        if (sceneAtticEnemy)
        {
            StartCoroutine(Enemy());
        }
    }
    void LoadProgressScene() 
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            itemNames = loadedProgress.itemNames;
            sceneAtticEnemy = loadedProgress.sceneAtticEnemy;

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
    IEnumerator Enemy() 
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefab, positionPlayer.position, Quaternion.identity);
    }
}
