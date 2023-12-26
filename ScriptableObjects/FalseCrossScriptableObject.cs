using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/FalseCrossScriptableObject")]
public class FalseCrossScriptableObject: ItemScriptableObject
{
    private DoorController doorController;
    private InventoryManager inventoryManager;
    private GameObject player;
    public GameObject backgroundDie;
    public GameObject axeDie;
    private TakeBook takeBook;
    public ItemScriptableObject cross;
    public override void InteractWithItem()
    {
        doorController = FindObjectOfType<DoorController>();
        inventoryManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InventoryManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        takeBook = FindObjectOfType<TakeBook>();
        if (doorController != null && doorController.isTrigger)
        {
            BlockKeys.DialogOpened();
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().Dead();
            player.transform.position = new Vector3(0, 0, 0);
            Instantiate(backgroundDie);
            Instantiate(axeDie);
        }
        if (takeBook != null && takeBook.isTrigger)
        {
            inventoryManager.AddItem(cross);
            SaveProgressScene();
            takeBook.sceneHallTakenCross = true;
            takeBook.isEnd = false;
            takeBook.TakeCross();
            inventoryManager.RemoveItem(this);
        }
    }
    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.itemNames.Add(cross.itemName);
        progress.itemNames.Remove(this.itemName);
        progress.sceneHallTakenCross = true;
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneHallTakenCross = progress.sceneHallTakenCross;
        currentProgress.itemNames.AddRange(progress.itemNames);
        currentProgress.itemNames.Remove(this.itemName);

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
        Debug.Log("Сохранили данные");
    }
}
