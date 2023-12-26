using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class FillingFieldsSaveLoad : MonoBehaviour
{
    [SerializeField]
    private string nameFile;
    private string nextScene;
    private DateTime saveDate;
    [SerializeField]
    private TMP_Text textSaveDate;
    private bool notEmpty;

    private void Start()
    {
        LoadProgressScene(nameFile);
        if (notEmpty)
        {
            textSaveDate.text = "Дата сохранения: " + saveDate.ToString("dd/MM/yyyy HH:mm:ss");
        }
        else
        {
            nextScene = "Courtyard";
            textSaveDate.text = "Пусто";
        }
        
    }
    public void LoadProgressField()
    {
        BlockKeys.DialogClosed();
        BlockKeys.InventoryClosed();
        if (nextScene == "Hallway")
        {
            SceneHallwayController.nameFile = nameFile;
        }
        SceneManager.LoadScene(nextScene);
    }

    public void LoadProgressScene(string nameFile)
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress(nameFile);

        if (loadedProgress != null)
        {
            nextScene = loadedProgress.nameScene;
            saveDate = loadedProgress.saveDate;
            notEmpty = true;
        }
        else
        {
            notEmpty = false;     
        }
    }
}
