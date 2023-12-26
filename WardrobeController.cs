using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WardrobeController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelInfo;
    [SerializeField]
    private StaticScriptableObject staticScriptableObject;
    [SerializeField]
    private bool isTrigger;
    private Animator animator;
    [SerializeField]
    private string[] desiredAnimationName;
    private string currentAnimationName;
    public static bool sceneWithFireplaceActionFire;
    public static bool sceneSecretRoomTakenWater;
    [SerializeField]
    private GameObject panelChoosingVessels;
    public static bool closePanelChoosingVessels;
    private void Start()
    {
        panelInfo.SetActive(false);
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        closePanelChoosingVessels = true;
        Debug.Log(sceneWithFireplaceActionFire);
    }
    private void Update()
    {
        if (!panelInfo.activeSelf)
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.inventoryOpen && !BlockKeys.dialogOpen && !sceneWithFireplaceActionFire)
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

            }
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.inventoryOpen && !BlockKeys.dialogOpen && sceneWithFireplaceActionFire && sceneSecretRoomTakenWater)
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

            }
        }
        else
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !sceneWithFireplaceActionFire)
            {
                panelInfo.SetActive(false);
                BlockKeys.DialogClosed();
            }
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && sceneWithFireplaceActionFire && sceneSecretRoomTakenWater)
            {
                panelInfo.SetActive(false);
                BlockKeys.DialogClosed();
            }
        }
        if (!panelChoosingVessels.activeInHierarchy && !BlockKeys.inventoryOpen)
        {
            if (isTrigger && sceneWithFireplaceActionFire && Input.GetKeyDown(KeyCode.Space) && !sceneSecretRoomTakenWater)
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
                    panelChoosingVessels.SetActive(true);
                }
            }
        }
        if (!closePanelChoosingVessels) 
        {
            panelChoosingVessels.SetActive(false);
            closePanelChoosingVessels = true;
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
}
