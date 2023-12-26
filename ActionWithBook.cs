using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionWithBook : MonoBehaviour
{
    public bool isTrigger;
    [SerializeField]
    public GameObject bookPanel;
    [SerializeField]
    private Button actionWithBook;
    public bool isEnd;
    private bool isMainOpen;
    private void Start()
    {
        isEnd = true;
        isMainOpen = false;
    }
    protected virtual void Update()
    {
        if (!bookPanel.activeInHierarchy && !isMainOpen) 
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && isEnd)
            {
                BlockKeys.DialogOpened();
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Speed", 0);
                float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
                float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
                if (vertical != 0 && horizontal != 0)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
                }
                isEnd = false;
                Debug.Log("на полке лежит книга");
                bookPanel.SetActive(true);
                actionWithBook.GetComponent<Button>().onClick.AddListener(ActionBook);
                isMainOpen = true;
            }
        }
        if (!bookPanel.activeInHierarchy && isMainOpen) 
        {
            isMainOpen = false;
        }
        
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
    protected virtual void IgnoreBook() 
    {
        bookPanel.SetActive(false);
        actionWithBook.GetComponent<Button>().onClick.RemoveListener(ActionBook);
        BlockKeys.DialogClosed();
    }
    protected virtual void ActionBook() 
    {
        actionWithBook.GetComponent<Button>().onClick.RemoveListener(ActionBook);
    }
}
