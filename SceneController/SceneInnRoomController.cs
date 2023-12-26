using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneInnRoomController : MonoBehaviour
{
    private bool scenePartCompleted;
    private bool sceneInnRoomLetterTaken;
    [SerializeField]
    private DialogManager dialog_1;
    [SerializeField]
    private GameObject panelDialog_1;
    [SerializeField]
    private DialogManager dialog_2;
    [SerializeField]
    private GameObject panelDialog_2;
    [SerializeField]
    private DialogManager dialog_3;
    [SerializeField]
    private GameObject panelDialog_3;
    private bool hasBlackoutBeenCalled = false;
    //private float positionX;
    //private float positionY;
    [SerializeField]
    private GameObject sheetsObject;
    [SerializeField]
    private GameObject movableObjectChair;
    [SerializeField]
    private string nextScene;
    public GameObject chairStatic;
    private bool letterTaken = false;
    //[SerializeField]
    //private Animator animatorScene;
    private GameObject player;
    private string nameFile = "/currentSession.dat";
    private List<string> itemNames = new List<string>();
    [SerializeField] private InventoryManager inventoryManager;
    private string nameSheetsObject;
    [SerializeField]
    private Transform positionPlayer;
    private bool sceneWithFireplaceActionFire;
    private bool sceneSecretRoomTakenWater;
    [SerializeField]
    private GameObject blackoutInnRoom;
    private bool sceneAtticEnemy;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        nameSheetsObject = sheetsObject.GetComponent<Item>().itemScrObj.itemName;
        LoadProgressScene();

        if (sceneInnRoomLetterTaken) 
        {
            movableObjectChair.SetActive(false);
            sheetsObject.SetActive(false);
            chairStatic.SetActive(true);
        }
        panelDialog_2.SetActive(false);
        if (!scenePartCompleted)
        {
            player.GetComponent<PlayerController>().isScriptActive = false;
            panelDialog_1.SetActive(true);
        }
        else
        {
            player.GetComponent<PlayerController>().isScriptActive = true;
            panelDialog_1.SetActive(false);
            player.transform.position = positionPlayer.position;
        }
        if (sceneAtticEnemy)
        {
            panelDialog_3.SetActive(true);
        }
        
    }
    void Update()
    {
        if (!scenePartCompleted) 
        {
            if (dialog_1.isDialogEnd && !hasBlackoutBeenCalled)
            {
                hasBlackoutBeenCalled = true;
                StartCoroutine(BlackoutScene());
            }
            if (dialog_2.isDialogEnd)
            {
                player.GetComponent<PlayerController>().isScriptActive = true;
                SaveProgressSceneSceneInnRoomSecret();
            }
        }
        
        if (sheetsObject == null && !letterTaken) 
        {
            letterTaken = true;
            SaveProgressSceneInnRoomLetterTaken();
        }
        if (sceneAtticEnemy && dialog_3.isDialogEnd)
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator BlackoutScene() 
    {
        BlockKeys.DialogOpened();
        Instantiate(blackoutInnRoom);
        yield return new WaitForSeconds(10f);
        panelDialog_2.SetActive(true);
    }
    IEnumerator EndGame()
    {
        BlockKeys.DialogOpened();
        Instantiate(blackoutInnRoom);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
    private void SaveProgressSceneSceneInnRoomSecret()
    {
        scenePartCompleted = true;
        GameProgress progress = new GameProgress();
        progress.sceneInnRoomSecret = scenePartCompleted;
        
        progress.nameFile = nameFile;
        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneInnRoomSecret = progress.sceneInnRoomSecret;
        Debug.Log($"scenePartCompleted = {scenePartCompleted}, sceneInnRoomSecret = {progress.sceneInnRoomSecret}, ");
        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    private void SaveProgressSceneInnRoomLetterTaken()
    {
        GameProgress progress = new GameProgress();
        progress.sceneInnRoomLetterTaken = true;
        progress.itemNames.Add(nameSheetsObject);
        progress.nameFile = nameFile;

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneInnRoomLetterTaken = progress.sceneInnRoomLetterTaken;
        currentProgress.itemNames.AddRange(progress.itemNames);

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    void LoadProgressScene()
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            scenePartCompleted = loadedProgress.sceneInnRoomSecret;
            sceneInnRoomLetterTaken = loadedProgress.sceneInnRoomLetterTaken;
            sceneWithFireplaceActionFire = loadedProgress.sceneWithFireplaceActionFire;
            itemNames = loadedProgress.itemNames;
            sceneSecretRoomTakenWater = loadedProgress.sceneSecretRoomTakenWater;
            sceneAtticEnemy = loadedProgress.sceneAtticEnemy;

            LoadInventory(itemNames);
            TableInnRoomController.sceneInnRoomLetterTaken = sceneInnRoomLetterTaken;
            WardrobeController.sceneWithFireplaceActionFire = sceneWithFireplaceActionFire;
            WardrobeController.sceneSecretRoomTakenWater = sceneSecretRoomTakenWater;
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

        Debug.Log("Load inventory");
    }
}
