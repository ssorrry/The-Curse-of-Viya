using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameProgress
{
    public DateTime saveDate;
    public string nameFile;
    public string nameScene;
    public bool sceneCourtyardAcquaintance; //первая сцена - знакомство с Сотником
    public bool sceneInnRoomSecret; //вторая сцена - стрнные звуки в коридоре
    public float positionX;
    public float positionY;
    public bool sceneInnRoomLetterTaken; //вторая сцена - взяты листы бумаги со стола 
    public List<string> itemNames = new List<string>();
    public bool sceneHallwayAppearanceSavePoint; //третья сцена - появление собаки (точки сохранения)
    public bool sceneHallDialogueWithCat; //четвертая сцена - разговор с кошкой
    public bool sceneHallInfoSecretRoom;
    public bool sceneSecretRoomOpen;
    public bool sceneWithFireplaceActionFire;
    public bool sceneSecretRoomTakenWater;
    public bool sceneWithFireplaceExtinguished;
    public bool sceneWithFireplaceTakenSoot;
    public bool sceneHallGavenAwaySoot;
    public bool sceneHallTakenCross;
    public bool sceneHallDoorOpen;
    public bool sceneAtticEnemy;
}
