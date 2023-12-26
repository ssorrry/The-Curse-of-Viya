using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TableInnRoomController : MonoBehaviour
{
    public static bool objectMoved = false;
    private bool isTrigger = false;
    [SerializeField]
    private GameObject panelDialogue;
    [SerializeField]
    private GameObject sheetsObject;
    [SerializeField]
    private GameObject staticTableObject;
    [SerializeField]
    private string[] desiredAnimationName;
    private string currentAnimationName;
    private Animator animator;
    public static bool sceneInnRoomLetterTaken;
    private void Start()
    {
        panelDialogue.SetActive(false);
        sheetsObject.SetActive(false);
        staticTableObject.SetActive(false);
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        
    }
    private void Update()
    {
        if (!sceneInnRoomLetterTaken) 
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !objectMoved)
            {
                currentAnimationName = GetCurrentAnimationName(animator);
                if (ArrayContains(desiredAnimationName, currentAnimationName))
                {
                    panelDialogue.SetActive(true);
                }
            }
            if (objectMoved && sheetsObject != null)
            {
                sheetsObject.SetActive(true);
            }
            if (sheetsObject == null)
            {
                staticTableObject.SetActive(true);
            }
        }
        else
        {
            staticTableObject.SetActive(true);
            sheetsObject.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
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
