using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionHallwaySecretRoom : BaseTransitionBetweenScenes
{
    private float positionX;
    private float positionY;
    private GameObject player;
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

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.positionX = progress.positionX;
        currentProgress.positionY = progress.positionY;

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
}
