using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveProgressButton : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private string nameFile;
    private DateTime saveDate;
    private bool notEmpty;
    [SerializeField]
    private TMP_Text textSaveDate;
    private string nameScene = "Hallway";
    private void Start()
    {
        FillingField();
    }
    public void SaveProgressField()
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");
        if (loadedProgress != null)
        {
            // Создание нового объекта GameProgress для сохранения данных
            GameProgress newProgress = new GameProgress();

            // Копирование данных из загруженного файла в новый объект
            newProgress.saveDate = DateTime.Now;
            newProgress.nameFile = nameFile;
            newProgress.nameScene = nameScene;
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

            // Копирование списка предметов
            newProgress.itemNames.AddRange(loadedProgress.itemNames);

            // Сохранение нового объекта GameProgress с новым именем файла
            SaveLoadManager.SaveGameProgress(newProgress);

            Debug.Log($"Data saved to {nameFile}");
            FillingField();
        }
        //GameProgress progress = new GameProgress();
        //progress.sceneCourtyardAcquaintance = SaveProgress.sceneCourtyardAcquaintance;
        //progress.sceneInnRoomSecret = SaveProgress.sceneInnRoomSecret;
        //progress.nameFile = nameFile;
        //progress.nameScene = SaveProgress.sceneName;
        //progress.positionX = SaveProgress.positionX;
        //progress.positionY = SaveProgress.positionY;
        //progress.sceneInnRoomLetterTaken = SaveProgress.sceneInnRoomLetterTaken;
        //progress.saveDate = DateTime.Now;
        //progress.sceneHallwayAppearanceSavePoint = SaveProgress.sceneHallwayAppearanceSavePoint;
        //progress.itemNames = SaveProgress.itemNames;
        
        //for (int i = 0; i < inventoryManager.slots.Count; i++)
        //{
        //    if (!inventoryManager.slots[i].isEmpty)
        //    {
        //        Debug.Log(inventoryManager.slots[i].itemScrObj.itemName);
        //        progress.itemNames.Add(inventoryManager.slots[i].itemScrObj.itemName);
        //    }

        //}

        //SaveLoadManager.SaveGameProgress(progress);
        //Debug.Log($"Date save in {nameFile}");

        //FillingField();
    }
    public void LoadProgressScene(string nameFile)
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            saveDate = loadedProgress.saveDate;
            notEmpty = true;
        }
        else
        {
            notEmpty = false;
        }
    }
    public void FillingField() 
    {
        LoadProgressScene(nameFile);
        if (notEmpty)
        {
            textSaveDate.text = "Дата сохранения: " + saveDate.ToString("dd/MM/yyyy HH:mm:ss");
        }
        else
        {
            textSaveDate.text = "Пусто";
        }
    }
}
