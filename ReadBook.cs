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
        Debug.Log("����� ������/����. ������");
        panelInfo.SetActive(true);
        TMP_Text objectTextInfo = panelInfo.transform.Find("Panel infoStaticObject").GetComponent<TMP_Text>();
        objectTextInfo.text = "���, ��� ���� ����� ����������� � �����, ����� � ������ ������� ���������� ����� ������� ������.";
        isOpen = true;
        sceneHallInfoSecretRoom = true;
        SaveProgressSceneHallInfoSecretRoom();
    }
    private void SaveProgressSceneHallInfoSecretRoom()
    {
        GameProgress progress = new GameProgress();
        progress.sceneHallInfoSecretRoom = sceneHallInfoSecretRoom;
        progress.nameFile = nameFile;

        // ��������� ������� �������� �� �����
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // ��������� ������ �������� ���������
        if (!currentProgress.sceneHallInfoSecretRoom)
        {
            currentProgress.sceneHallInfoSecretRoom = progress.sceneHallInfoSecretRoom;
        }

        // ��������� ����������� ������ � ����
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    protected override void IgnoreBook()
    {
        base.IgnoreBook();
        isEnd = true;
    }
}
