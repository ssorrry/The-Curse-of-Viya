using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController: MonoBehaviour
{
    private bool isTrigger;
    [SerializeField]
    private GameObject panelChoose;
    private bool isOpen = false;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform positionEnemy;
    [SerializeField]
    private Sprite picture;
    private void Update()
    {
        if (!panelChoose.activeInHierarchy && !isOpen)
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space))
            {
                BlockKeys.DialogOpened();
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Speed", 0);
                float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
                float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
                if (vertical != 0 && horizontal != 0)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
                }
                panelChoose.SetActive(true);
                isOpen = true;
            }
        }
        else if(!panelChoose.activeInHierarchy && isOpen)
        { 
            isOpen = false;
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
    public void IgnoreScreen() 
    {
        panelChoose.SetActive(false);
        BlockKeys.DialogClosed();
        
    }
    public void CreateEnemy() 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = picture;
        IgnoreScreen();
        Instantiate(enemyPrefab, positionEnemy.position, Quaternion.identity);
        SaveProgressScene();
        SceneAtticController.chase = true;
    }
    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.sceneAtticEnemy = true;

        progress.nameFile = "/currentSession.dat";
        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneAtticEnemy = progress.sceneAtticEnemy;
        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
}
