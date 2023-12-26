using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneHallwayController : MonoBehaviour
{
    public bool sceneHallwayAppearanceSavePoint;
    public bool sceneHallDialogueWithCat;
    public GameObject panelSaveGame;
    [SerializeField]
    private DialogManager dialog;
    private bool isSave;
    [SerializeField]
    private GameObject npc;
    [SerializeField]
    private GameObject savePoint;
    protected GameObject player;
    protected List<string> itemNames = new List<string>();
    [SerializeField] protected InventoryManager inventoryManager;
    public static string nameFile;
    private float positionX;
    private float positionY;
    private bool sceneHallInfoSecretRoom;
    private bool sceneSecretRoomOpen;
    [SerializeField]
    private GameObject enemyPrefab;
    private bool sceneAtticEnemy;
    [SerializeField]
    private Transform positionEnemy1;
    [SerializeField]
    private Transform positionEnemy2;
    [SerializeField]
    private Transform positionEnemy3;
    public static bool enemyFromFireplace;
    public static bool enemyFromSecretRoom;
    [SerializeField]
    private BloodController bloodController;
    private void Awake()
    {
        Debug.Log(nameFile);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (nameFile != null)
        {
            LoadProgressScene(nameFile);
        }
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = true;
        
        if (positionX != 0 && positionY != 0) 
        {
            player.transform.position = new Vector3(positionX, positionY, 0);
        }
        if (sceneHallwayAppearanceSavePoint)
        {
            npc.SetActive(false);
            savePoint.SetActive(true);
        }

        if (sceneAtticEnemy)
        {
            savePoint.SetActive(false);
            if (enemyFromFireplace)
            {
                enemyFromFireplace = false;
                StartCoroutine(Enemy(positionEnemy2));
            }
            else if (enemyFromSecretRoom)
            {
                enemyFromSecretRoom = false;
                StartCoroutine(Enemy(positionEnemy3));
            }
            else 
            {
                StartCoroutine(Enemy(positionEnemy1));
            }

        }
    }
    void Update()
    {
        if (!sceneHallwayAppearanceSavePoint)
        {
            if (dialog.isDialogEnd && !isSave) 
            {
                isSave = true;
                BlockKeys.DialogOpened();
                sceneHallwayAppearanceSavePoint = true;
                npc.SetActive(false);
                savePoint.SetActive(true);
                panelSaveGame.SetActive(true);
                SaveProgressSceneHallwayAppearanceSavePoint();
            }
            //открыывается дверь в зал
        }
        if (!panelSaveGame.activeInHierarchy && isSave) 
        {
            BlockKeys.DialogClosed();
        }
    }
    private void SaveProgressSceneHallwayAppearanceSavePoint() 
    {
        GameProgress progress = new GameProgress();
        progress.sceneHallwayAppearanceSavePoint = true;
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneHallwayAppearanceSavePoint = progress.sceneHallwayAppearanceSavePoint;

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);

    }
    void LoadProgressScene(string nameFile) 
    {
        if (nameFile != "/currentSession.dat")
        {
            DeleteFile("/currentSession.dat");
            GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);
            if (loadedProgress != null)
            {
                // Создаем новый объект GameProgress для сохранения данных
                GameProgress newProgress = new GameProgress();

                // Копируем данные из загруженного файла в новый объект
                newProgress.saveDate = loadedProgress.saveDate;
                newProgress.nameFile = "/currentSession.dat";
                newProgress.nameScene = loadedProgress.nameScene;
                newProgress.sceneCourtyardAcquaintance = loadedProgress.sceneCourtyardAcquaintance;
                newProgress.sceneInnRoomSecret = loadedProgress.sceneInnRoomSecret;
                newProgress.positionX = loadedProgress.positionX;
                newProgress.positionY = loadedProgress.positionY;
                newProgress.sceneInnRoomLetterTaken = loadedProgress.sceneInnRoomLetterTaken;
                newProgress.sceneHallwayAppearanceSavePoint = loadedProgress.sceneHallwayAppearanceSavePoint;
                newProgress.sceneHallDialogueWithCat = loadedProgress.sceneHallDialogueWithCat;
                newProgress.sceneHallInfoSecretRoom = loadedProgress.sceneHallInfoSecretRoom;
                newProgress.sceneSecretRoomOpen = loadedProgress.sceneSecretRoomOpen;
                newProgress.sceneWithFireplaceActionFire = loadedProgress.sceneWithFireplaceActionFire;
                newProgress.sceneSecretRoomTakenWater = loadedProgress.sceneSecretRoomTakenWater;
                newProgress.sceneWithFireplaceExtinguished = loadedProgress.sceneWithFireplaceExtinguished;
                newProgress.sceneWithFireplaceTakenSoot = loadedProgress.sceneWithFireplaceTakenSoot;
                newProgress.sceneHallGavenAwaySoot = loadedProgress.sceneHallGavenAwaySoot;
                newProgress.sceneHallTakenCross = loadedProgress.sceneHallTakenCross;
                newProgress.sceneHallDoorOpen = loadedProgress.sceneHallDoorOpen;


                Debug.Log($"newProgress.sceneWithFireplaceActionFire = {newProgress.sceneWithFireplaceActionFire}, loadedProgress.sceneWithFireplaceActionFire = {loadedProgress.sceneWithFireplaceActionFire}");
                // Копируем список предметов
                newProgress.itemNames.AddRange(loadedProgress.itemNames);

                // Сохраняем новый объект GameProgress
                SaveLoadManager.SaveGameProgress(newProgress);

                // Обновляем текущий объект, если это необходимо
                itemNames.AddRange(loadedProgress.itemNames);
                positionX = loadedProgress.positionX;
                positionY = loadedProgress.positionY;
                sceneHallwayAppearanceSavePoint = loadedProgress.sceneHallwayAppearanceSavePoint;
                sceneHallInfoSecretRoom = loadedProgress.sceneHallInfoSecretRoom;
                sceneSecretRoomOpen = loadedProgress.sceneSecretRoomOpen;
                sceneHallDialogueWithCat = loadedProgress.sceneHallDialogueWithCat;

                LoadInventory(itemNames);
                PassageSecretRoomController.sceneHallInfoSecretRoom = sceneHallInfoSecretRoom;
                PassageSecretRoomController.sceneSecretRoomOpen = sceneSecretRoomOpen;
            }
        }
        else 
        {
            GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

            if (loadedProgress != null)
            {
                itemNames.AddRange(loadedProgress.itemNames);
                sceneHallDialogueWithCat = loadedProgress.sceneHallDialogueWithCat;
                positionX = loadedProgress.positionX;
                positionY = loadedProgress.positionY;
                sceneHallwayAppearanceSavePoint = loadedProgress.sceneHallwayAppearanceSavePoint;
                sceneHallInfoSecretRoom = loadedProgress.sceneHallInfoSecretRoom;
                sceneSecretRoomOpen = loadedProgress.sceneSecretRoomOpen;
                sceneAtticEnemy = loadedProgress.sceneAtticEnemy;
                bloodController.sceneAtticEnemy = sceneAtticEnemy;

                PassageSecretRoomController.sceneHallInfoSecretRoom = sceneHallInfoSecretRoom;
                PassageSecretRoomController.sceneSecretRoomOpen = sceneSecretRoomOpen;

                LoadInventory(itemNames);
            }
        }
        
    }
    private void LoadInventory(List<string> array)
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

        Debug.Log("Load inventory");
    }
    private void DeleteFile(string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;
        // Проверяем, существует ли файл
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Файл удален: " + fileName);
        }
        else
        {
            Debug.LogWarning("Файл не существует: " + fileName);
        }
    }
    IEnumerator Enemy(Transform transform)
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
