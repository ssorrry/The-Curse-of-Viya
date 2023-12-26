using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoomWithFireplaceController : MonoBehaviour
{
    private GameObject player;
    private string nameFile = "/currentSession.dat";
    private List<string> itemNames = new List<string>();
    [SerializeField]
    private InventoryManager inventoryManager;
    public bool sceneWithFireplaceActionFire;
    public bool sceneWithFireplaceExtinguished;
    private bool sceneWithFireplaceTakenSoot;
    [SerializeField]
    private GameObject soot;
    private bool isTaken;
    private string nameSoot;
    [SerializeField]
    private GameObject enemyPrefab;
    private bool sceneAtticEnemy;
    [SerializeField]
    private Transform positionEnemy;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadProgressScene();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = true;
        if (sceneWithFireplaceTakenSoot)
        {
            soot.SetActive(false);
        }
        nameSoot = soot.GetComponent<Item>().itemScrObj.itemName;
        if (sceneAtticEnemy)
        {
            StartCoroutine(Enemy());
        }
    }
    private void Update()
    {
        if (soot != null)
        {
            if (!soot.activeInHierarchy && sceneWithFireplaceExtinguished && !sceneWithFireplaceTakenSoot) 
            {
                soot.SetActive(true);
            }
        }
        else if (!isTaken)
        {
            isTaken = true;
            SaveProgressScene();
        }
        
    }

    void SaveProgressScene() 
    {
        GameProgress progress = new GameProgress();
        progress.sceneWithFireplaceTakenSoot = true;
        progress.itemNames.Add(nameSoot);
        progress.nameFile = nameFile;

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneWithFireplaceTakenSoot = progress.sceneWithFireplaceTakenSoot;
        currentProgress.itemNames.AddRange(progress.itemNames);

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
        
    }

    public void LoadProgressScene()
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            itemNames = loadedProgress.itemNames;
            sceneWithFireplaceActionFire = loadedProgress.sceneWithFireplaceActionFire;
            sceneWithFireplaceExtinguished = loadedProgress.sceneWithFireplaceExtinguished;
            sceneWithFireplaceTakenSoot = loadedProgress.sceneWithFireplaceTakenSoot;
            sceneAtticEnemy = loadedProgress.sceneAtticEnemy;

            LoadInventory(itemNames);
        }
    }
    public void LoadFireplaceExtinguished() 
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            sceneWithFireplaceExtinguished = loadedProgress.sceneWithFireplaceExtinguished;
        }
    }
    protected void LoadInventory(List<string> array)
    {
        if (array != null)
        {
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
                        inventoryManager.slots[i].itemName.color = new Color(1f, 1f, 1f, 1f);
                        inventoryManager.slots[i].itemIcon.color = new Color(1f, 1f, 1f, 1f);
                        break;

                    }
                }
            }
        }
    }
    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefab, positionEnemy.position, Quaternion.identity);
        SceneHallwayController.enemyFromFireplace = true;
    }
}
