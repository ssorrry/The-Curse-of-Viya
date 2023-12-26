using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireplaceController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelInfo;
    [SerializeField]
    private StaticScriptableObject staticScriptableObject;
    public bool isTrigger;
    private Animator animator;
    [SerializeField]
    private string[] desiredAnimationName;
    private string currentAnimationName;
    [SerializeField]
    private SceneRoomWithFireplaceController sceneRoomWithFireplaceController;
    [SerializeField]
    private Sprite fireplace;
    private GameObject fireplaceObject;
    private void Start()
    {
        panelInfo.SetActive(false);
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        if (sceneRoomWithFireplaceController.sceneWithFireplaceExtinguished) 
        {
            fireplaceObject = GameObject.FindGameObjectWithTag("Fireplace");
            fireplaceObject.GetComponent<SpriteRenderer>().sprite = fireplace;
        }
    }

    private void Update()
    {
        if (!panelInfo.activeSelf && !sceneRoomWithFireplaceController.sceneWithFireplaceExtinguished)
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.inventoryOpen && !BlockKeys.dialogOpen)
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
                    objectTextInfo.text = "";
                    objectTextInfo.text = staticScriptableObject.infoStaticObject;
                }
                if (!sceneRoomWithFireplaceController.sceneWithFireplaceActionFire) 
                {
                    SaveProgressSceneWithFireplaceActionFire();
                }

            }
        }
        else
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !sceneRoomWithFireplaceController.sceneWithFireplaceExtinguished)
            {
                panelInfo.SetActive(false);
                BlockKeys.DialogClosed();
            }
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            //other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
            //other.transform.GetChild(0).gameObject.SetActive(false);
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
    private void SaveProgressSceneWithFireplaceActionFire() 
    {
        GameProgress progress = new GameProgress();
        progress.sceneWithFireplaceActionFire = true;
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneWithFireplaceActionFire = progress.sceneWithFireplaceActionFire;
        Debug.Log($"В методе сохранения = {currentProgress.sceneWithFireplaceActionFire}");

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
}
