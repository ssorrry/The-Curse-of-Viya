using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SceneSecretRoomController: MonoBehaviour
{
    private GameObject player;
    private bool sceneSecretRoomOpen;
    protected List<string> itemNames = new List<string>();
    [SerializeField]
    private InventoryManager inventoryManager;
    private string nameFile = "/currentSession.dat";
    [SerializeField]
    private GameObject enemyPrefab;
    private bool sceneAtticEnemy;
    [SerializeField]
    private Transform positionEnemy;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!sceneSecretRoomOpen) 
        {
            SaveProgressSceneSecretRoomOpen();
        }
    }
    private void Start()
    {
        LoadProgressScene();
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = true;
        if (sceneAtticEnemy)
        {
            StartCoroutine(Enemy());
        }
    }
    private void SaveProgressSceneSecretRoomOpen()
    {
        GameProgress progress = new GameProgress();
        progress.sceneSecretRoomOpen = true;
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneSecretRoomOpen = progress.sceneSecretRoomOpen;

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    void LoadProgressScene()
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            itemNames = loadedProgress.itemNames;
            sceneSecretRoomOpen = loadedProgress.sceneSecretRoomOpen;
            sceneAtticEnemy = loadedProgress.sceneAtticEnemy;

            LoadInventory(itemNames);
        }
    }
    protected void LoadInventory(List<string> array)
    {
        if (array != null)
        {
            for (int j = 0; j < array.Count; j++)
            {
                Debug.Log(inventoryManager.slots.Count);
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>($"Item/{array[j]}");
                for (int i = 0; i < inventoryManager.slots.Count; i++)
                {
                    Debug.Log("for (int i = 0; i < inventoryManager.slots.Count; i++)");
                    if (inventoryManager.slots[i].isEmpty)
                    {
                        Debug.Log("if (inventoryManager.slots[i].isEmpty)");
                        inventoryManager.slots[i].itemScrObj = item;
                        inventoryManager.slots[i].itemName.text = item.itemName;
                        inventoryManager.slots[i].itemDescription.text = item.itemDescription;
                        inventoryManager.slots[i].itemIcon.sprite = item.itemIcon;
                        inventoryManager.slots[i].isEmpty = false;
                        inventoryManager.slots[i].itemName.color = new Color(1f, 1f, 1f, 1f);
                        inventoryManager.slots[i].itemIcon.color = new Color(1f, 1f, 1f, 1f);
                        break;
                    }
                }
            }
        }

        Debug.Log("Load inventory");
    }
    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefab, positionEnemy.position, Quaternion.identity);
        SceneHallwayController.enemyFromSecretRoom = true;
    }
}
