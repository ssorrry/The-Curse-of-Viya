using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionHallwayHall : BaseTransitionBetweenScenes
{
    //private string nameFile = "/currentSession.dat";
    [SerializeField]
    private SceneHallwayController sceneHallwayController;
    private Animator animator;
    private GameObject player;
    private float positionX;
    private float positionY;
    [SerializeField]
    private GameObject panelInfo;
    [SerializeField]
    private string[] desiredAnimationName;
    private string currentAnimationName;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }
    protected override void Update()
    {
        //if (isTrigger && !sceneHallwayController.sceneHallwayAppearanceSavePoint)
        //{
        //    staticObjectDoor.SetActive(true);
        //}
        //else if (isTrigger && sceneHallwayController.sceneHallwayAppearanceSavePoint && Input.GetKeyDown(KeyCode.Space))
        //{
        //    staticObjectDoor.SetActive(false);
        //    positionX = player.transform.position.x;
        //    positionY = player.transform.position.y;
        //    SaveProgressScenePlayerPosition();
        //    SceneManager.LoadScene(nameNextScene);
        //}
        if (sceneHallwayController.sceneHallwayAppearanceSavePoint)
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen) 
            {
                positionX = player.transform.position.x;
                positionY = player.transform.position.y;
                SaveProgressScenePlayerPosition();
                SceneManager.LoadScene(nameNextScene);
            }
        }
        else
        {
            if (!panelInfo.activeInHierarchy)
            {
                if (isTrigger && Input.GetKeyDown(KeyCode.Space))
                {
                    currentAnimationName = GetCurrentAnimationName(animator);
                    if (ArrayContains(desiredAnimationName, currentAnimationName))
                    {
                        BlockKeys.DialogOpened();
                        animator.SetFloat("Speed", 0);
                        float vertical = animator.GetFloat("Vertical");
                        float horizontal = animator.GetFloat("Horizontal");
                        if (vertical != 0 && horizontal != 0)
                        {
                            animator.SetFloat("Vertical", 0);
                        }
                        panelInfo.SetActive(true);
                        TMP_Text objectTextInfo = panelInfo.transform.Find("Panel infoStaticObject").GetComponent<TMP_Text>();
                        objectTextInfo.text = "Дверь закрыта";
                    }
                }
            }
            else
            {
                if (isTrigger && Input.GetKeyDown(KeyCode.Space))
                {
                    panelInfo.SetActive(false);
                    BlockKeys.DialogClosed();
                }
            }
        }
    }
    private void SaveProgressScenePlayerPosition()
    {
        GameProgress progress = new GameProgress();
        progress.positionX = positionX;
        progress.positionY = positionY;

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        currentProgress.positionX = progress.positionX;
        currentProgress.positionY = progress.positionY;

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
    private string GetCurrentAnimationName(Animator animator)
    {
        if (animator != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

            if (clipInfo.Length > 0)
            {
                return clipInfo[0].clip.name;
            }
        }

        return "Анимация не найдена";
    }
    private bool ArrayContains(string[] array, string value)
    {
        foreach (string item in array)
        {
            if (item == value)
            {
                return true;
            }
        }
        return false;
    }
}
