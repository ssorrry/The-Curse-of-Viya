using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemScriptableObject itemScrObj;
    private bool isTrigger;
    [SerializeField]
    private GameObject choosingTakeItem;
    private bool isOpen;
    private void Update()
    {
        if (!choosingTakeItem.activeInHierarchy && !isOpen) 
        {
            if (isTrigger && (Input.GetKeyDown(KeyCode.Space)))
            {
                choosingTakeItem.SetActive(true);
                choosingTakeItem.GetComponent<ChoosingTakeItem>().actionText.text = $"Âחÿעü ןנוהלוע: {itemScrObj.itemName}?";
                BlockKeys.DialogOpened();
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Speed", 0);
                float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
                float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
                if (vertical != 0 && horizontal != 0)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
                }
                isOpen = true;
            }
        }
        if (!choosingTakeItem.activeInHierarchy && isOpen) 
        {
            isOpen = false;
        }
        if (choosingTakeItem.GetComponent<ChoosingTakeItem>().isTake && choosingTakeItem.activeSelf == false)
        {
            BlockKeys.DialogClosed();
            GameObject.FindWithTag("Canvas").GetComponent<InventoryManager>().AddItem(itemScrObj);
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            Debug.Log("OnTriggerEnter2D");
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
            Debug.Log("OnTriggerExit2D");
        }
    }
}
