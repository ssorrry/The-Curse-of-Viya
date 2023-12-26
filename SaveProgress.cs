using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProgress : MonoBehaviour
{
    public static bool sceneCourtyardAcquaintance;
    public static bool sceneInnRoomSecret;
    public static string sceneName;
    public static float positionX;
    public static float positionY;
    [SerializeField]
    private GameObject panelSaveGame;
    public static bool sceneInnRoomLetterTaken;
    public static bool sceneHallwayAppearanceSavePoint;
    public static List<string> itemNames = new List<string>();
    public void CloseSavePanel() 
    { 
        panelSaveGame.SetActive(false);
        BlockKeys.DialogClosed();
    }
    //public void SaveProgressFirstField() 
    //{
    //    GameProgress progress = new GameProgress();
    //    progress.sceneCourtyardAcquaintance = sceneCourtyardAcquaintance;
    //    progress.sceneInnRoomSecret = sceneInnRoomSecret;
    //    progress.nameFile = "/gameProgress1.dat";
    //    progress.nameScene = sceneName;
    //    progress.positionX = positionX;
    //    progress.positionY = positionY;
    //    progress.saveDate = DateTime.Now;

    //    SaveLoadManager.SaveGameProgress(progress);
    //    Debug.Log("Date save in File1");
    //}
    //public void SaveProgressSecondField() 
    //{
    //    GameProgress progress = new GameProgress();
    //    progress.sceneCourtyardAcquaintance = sceneCourtyardAcquaintance;
    //    progress.sceneInnRoomSecret = sceneInnRoomSecret;
    //    progress.nameFile = "/gameProgress2.dat";
    //    progress.nameScene = sceneName;
    //    progress.positionX = positionX;
    //    progress.positionY = positionY;
    //    progress.saveDate = DateTime.Now;

    //    SaveLoadManager.SaveGameProgress(progress);
    //    Debug.Log("Date save in File2");
    //}  
    //public void SaveProgressThirdField() 
    //{
    //    GameProgress progress = new GameProgress();
    //    progress.sceneCourtyardAcquaintance = sceneCourtyardAcquaintance;
    //    progress.sceneInnRoomSecret = sceneInnRoomSecret;
    //    progress.nameFile = "/gameProgress3.dat";
    //    progress.nameScene = sceneName;
    //    progress.positionX = positionX;
    //    progress.positionY = positionY;
    //    progress.saveDate = DateTime.Now;

    //    SaveLoadManager.SaveGameProgress(progress);
    //    Debug.Log("Date save in File3");
    //}
    //public void SaveProgressFourthField() 
    //{
    //    GameProgress progress = new GameProgress();
    //    progress.sceneCourtyardAcquaintance = sceneCourtyardAcquaintance;
    //    progress.sceneInnRoomSecret = sceneInnRoomSecret;
    //    progress.nameFile = "/gameProgress4.dat";
    //    progress.nameScene = sceneName;
    //    progress.positionX = positionX;
    //    progress.positionY = positionY;
    //    progress.saveDate = DateTime.Now;

    //    SaveLoadManager.SaveGameProgress(progress);
    //    Debug.Log("Date save in File4");
    //}
}
