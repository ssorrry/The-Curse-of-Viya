using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionHallwayRoomWithFireplace: BaseTransitionBetweenScenes
{
    [SerializeField]
    private SceneHallwayController sceneHallwayController;
    [SerializeField]
    private string[] desiredAnimationName;
    private string currentAnimationName;
    private Animator animator;
    [SerializeField]
    private GameObject panelInfo;
    private GameObject player;
    private float positionX;
    private float positionY;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }
    protected override void Update()
    {
        if (sceneHallwayController.sceneHallDialogueWithCat) 
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
                        objectTextInfo.text = "����� �������";
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

        return "�������� �� �������";
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
