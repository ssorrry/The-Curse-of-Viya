using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private GameObject panelSaveProgress;
    private bool isTrigger;
    [SerializeField]
    private string nameScene;
    private bool isOpen = false;
    private float positionX;
    private float positionY;
    private GameObject player;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    private void Update()
    {
        if (!panelSaveProgress.activeInHierarchy) 
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !isOpen) 
            {
                panelSaveProgress.SetActive(true);
                BlockKeys.DialogOpened();
                animator.SetFloat("Speed", 0);
                float vertical = animator.GetFloat("Vertical");
                float horizontal = animator.GetFloat("Horizontal");
                if (vertical != 0 && horizontal != 0)
                {
                    animator.SetFloat("Vertical", 0);
                }
                positionX = player.transform.position.x;
                positionY = player.transform.position.y;
                SaveProgressScenePlayerPosition();
                isOpen = true;
            }
        }
        if (!panelSaveProgress.activeInHierarchy && isOpen)
        {
            isOpen = false;
            BlockKeys.DialogClosed();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
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
