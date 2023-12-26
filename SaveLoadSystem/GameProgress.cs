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
    public bool sceneCourtyardAcquaintance; //������ ����� - ���������� � ��������
    public bool sceneInnRoomSecret; //������ ����� - ������� ����� � ��������
    public float positionX;
    public float positionY;
    public bool sceneInnRoomLetterTaken; //������ ����� - ����� ����� ������ �� ����� 
    public List<string> itemNames = new List<string>();
    public bool sceneHallwayAppearanceSavePoint; //������ ����� - ��������� ������ (����� ����������)
    public bool sceneHallDialogueWithCat; //��������� ����� - �������� � ������
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
