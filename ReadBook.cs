using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadBook: ActionWithBook
{
    [SerializeField]
    private GameObject panelInfo;
    private bool isOpen;
    private bool sceneHallInfoSecretRoom;
    private string nameFile = "/currentSession.dat";

    protected override void Update()
    {
        base.Update();
        if (panelInfo.activeInHierarchy && Input.GetKeyDown(KeyCode.Space) && !isEnd && !isOpen) 
        {
            panelInfo.SetActive(false);
            bookPanel.SetActive(false);
            isEnd = true;
            BlockKeys.DialogClosed();
        }
        if (panelInfo.activeInHierarchy && isOpen)
        {
            isOpen = false;
        }
    }
    protected override void ActionBook() 
    {
        base.ActionBook();
        bookPanel.SetActive(false);
        Debug.Log("вывод текста/стат. объект");
        panelInfo.SetActive(true);
        TMP_Text objectTextInfo = panelInfo.transform.Find("Panel infoStaticObject").GetComponent<TMP_Text>();
        objectTextInfo.text = "Там, где лучи света встречаются с тенью, дверь к тайной комнате раскроется перед глазами твоими.";
        isOpen = true;
        sceneHallInfoSecretRoom = true;
        SaveProgressSceneHallInfoSecretRoom();
    }
    private void SaveProgressSceneHallInfoSecretRoom()
    {
        GameProgress progress = new GameProgress();
        progress.sceneHallInfoSecretRoom = sceneHallInfoSecretRoom;
        progress.nameFile = nameFile;

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        if (!currentProgress.sceneHallInfoSecretRoom)
        {
            currentProgress.sceneHallInfoSecretRoom = progress.sceneHallInfoSecretRoom;
        }

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    protected override void IgnoreBook()
    {
        base.IgnoreBook();
        isEnd = true;
    }
}
