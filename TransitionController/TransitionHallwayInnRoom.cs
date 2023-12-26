using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionHallwayInnRoom : BaseTransitionBetweenScenes
{
    private GameObject player;
    private float positionX;
    private float positionY;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen)
        {
            positionX = player.transform.position.x;
            positionY = player.transform.position.y;
            SaveProgressScenePlayerPosition();
            SceneManager.LoadScene(nameNextScene);
        }
    }
    private void SaveProgressScenePlayerPosition()
    {
        GameProgress progress = new GameProgress();
        progress.positionX = positionX;
        progress.positionY = positionY;

        // ��������� ������� �������� �� �����
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // ��������� ������ �������� ���������
        currentProgress.positionX = progress.positionX;
        currentProgress.positionY = progress.positionY;

        // ��������� ����������� ������ � ����
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
}
