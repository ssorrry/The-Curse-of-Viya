using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneHallController : MonoBehaviour
{
    private GameObject player;
    private string nameFile = "/currentSession.dat";
    private List<string> itemNames = new List<string>();
    [SerializeField] 
    private InventoryManager inventoryManager;
    [SerializeField]
    private DialogManager dialog_1;
    private bool sceneHallDialogueWithCat;
    [SerializeField]
    private ActionWithCat actionWithCat;
    [SerializeField]
    private TakeBook takeBook;
    [SerializeField]
    private GameObject bigCat;
    public bool sceneHallGavenAwaySoot;
    private bool sceneHallTakenCross;
    private bool sceneHallDoorOpen;
    [SerializeField]
    private DoorController doorController;
    [SerializeField]
    private Transform positionPlayer;
    public static bool transitionFromLadder;
    [SerializeField]
    private GameObject enemyPrefab;
    private bool sceneAtticEnemy;
    [SerializeField]
    private GameObject bibliya;
    [SerializeField]
    private Sprite bibliyaCross;
    [SerializeField]
    private GameObject crest;

    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = true;
        if (transitionFromLadder)
        {
            transitionFromLadder = false;
            player.transform.position = new Vector3(positionPlayer.position.x, positionPlayer.position.y, positionPlayer.position.z);
        }
        LoadProgressScene();
        
        if (sceneHallGavenAwaySoot) 
        {
            actionWithCat.gameObject.SetActive(false);
            bigCat.SetActive(true);
            takeBook.sceneHallTakenCross = sceneHallTakenCross;
        }
        else
        {
            actionWithCat.sceneHallDialogueWithCat = sceneHallDialogueWithCat;
            actionWithCat.sceneHallGavenAwaySoot = sceneHallGavenAwaySoot;
        }
        doorController.sceneHallDoorOpen = sceneHallDoorOpen;
        if (sceneHallTakenCross)
        {
            bibliya.GetComponent<SpriteRenderer>().sprite = bibliyaCross;
        }
        if (sceneHallDoorOpen)
        {
            crest.SetActive(true);
        }
        if (sceneAtticEnemy)
        {
            StartCoroutine(Enemy());
        }
    }
    private void Update()
    {
        if (dialog_1.isDialogEnd && !sceneHallDialogueWithCat)
        {
            SaveProgressSceneSceneHallDialogueWithCat();
            actionWithCat.sceneHallDialogueWithCat = sceneHallDialogueWithCat;
        }
    }
    private void SaveProgressSceneSceneHallDialogueWithCat()
    {
        sceneHallDialogueWithCat = true;
        GameProgress progress = new GameProgress();
        progress.sceneHallDialogueWithCat = sceneHallDialogueWithCat;

        progress.nameFile = nameFile;
        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneHallDialogueWithCat = progress.sceneHallDialogueWithCat;
        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    void LoadProgressScene()
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            sceneHallDialogueWithCat = loadedProgress.sceneHallDialogueWithCat;
            sceneHallGavenAwaySoot = loadedProgress.sceneHallGavenAwaySoot;
            sceneHallTakenCross = loadedProgress.sceneHallTakenCross;
            sceneHallDoorOpen = loadedProgress.sceneHallDoorOpen;
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
                    Debug.Log("for (int i = 0; i < inventoryManager.slots.Count; i++)");
                }
                Debug.Log("for (int j = 0; j < array.Count; j++)");
            }
            Debug.Log("if (array != null)");

        }

        
    }
    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefab, positionPlayer.position, Quaternion.identity);
    }
}
