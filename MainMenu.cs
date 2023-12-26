using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using TMPro;

public class MainMenu: MonoBehaviour
{
    public static string nameFile;
    //private string nextScene;
    //private DateTime saveDate;
    //private TMP_Text[] textSaveDate;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartNewGame() 
    {
        SceneManager.LoadScene("Courtyard");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    //public void LoadProgressFirstField() 
    //{
    //    nameFile = "/gameProgress1.dat";
    //    LoadProgressScene(nameFile);
    //    SceneManager.LoadScene(nextScene);
    //}
    //public void LoadProgressSecondField() 
    //{
    //    nameFile = "/gameProgress2.dat";
    //    LoadProgressScene(nameFile);
    //    SceneManager.LoadScene(nextScene);
    //}
    //public void LoadProgressThirdField()    
    //{
    //    nameFile = "/gameProgress3.dat";
    //    LoadProgressScene(nameFile);
    //    SceneManager.LoadScene(nextScene);
    //}
    //public void LoadProgressFourthField() 
    //{
    //    nameFile = "/gameProgress4.dat";
    //    LoadProgressScene(nameFile);
    //    SceneManager.LoadScene(nextScene);
    //}

    //public void LoadProgressScene(string nameFile)
    //{
    //    GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

    //    if (loadedProgress != null)
    //    {
    //        nextScene = loadedProgress.nameScene;
    //        saveDate = loadedProgress.saveDate;
    //    }
    //}

}
