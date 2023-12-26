using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/VesselWithWaterScriptableObject")]
public class VesselWithWaterScriptableObject : ItemScriptableObject
{
    private bool sceneSecretRoomTakenWater;
    private FireplaceController fireplaceController;
    public Sprite fireplace;
    private GameObject fireplaceObject;
    private InventoryManager inventoryManager;
    public ItemScriptableObject glassVessel;
    private SceneRoomWithFireplaceController sceneRoomWithFireplaceController;
    public override void InteractWithItem()
    {
        LoadProgressScene();
        if (sceneSecretRoomTakenWater) 
        {
            fireplaceController = FindObjectOfType<FireplaceController>();
            sceneRoomWithFireplaceController = FindObjectOfType<SceneRoomWithFireplaceController>();
            inventoryManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InventoryManager>();
            fireplaceObject = GameObject.FindGameObjectWithTag("Fireplace");
            if (fireplaceController != null && fireplaceController.isTrigger)
            {
                fireplaceObject.GetComponent<SpriteRenderer>().sprite = fireplace;
                inventoryManager.AddItem(glassVessel);
                SaveProgressScene();
                sceneRoomWithFireplaceController.LoadFireplaceExtinguished();
                inventoryManager.RemoveItem(this);
            }
        }
    }
    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.sceneSecretRoomTakenWater = true;
        progress.itemNames.Add(glassVessel.itemName);
        progress.itemNames.Remove(this.itemName);
        progress.sceneWithFireplaceExtinguished = true;
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneSecretRoomTakenWater = progress.sceneSecretRoomTakenWater;
        currentProgress.itemNames.AddRange(progress.itemNames);
        currentProgress.itemNames.Remove(this.itemName);
        currentProgress.sceneWithFireplaceExtinguished = progress.sceneWithFireplaceExtinguished;

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
        Debug.Log("Сохранили данные");
    }
    public void LoadProgressScene() 
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        if (loadedProgress != null)
        {
            sceneSecretRoomTakenWater = loadedProgress.sceneSecretRoomTakenWater;
            Debug.Log("Загрузили данные");
        }
    }
}
