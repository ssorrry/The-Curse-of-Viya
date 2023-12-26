using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController: MonoBehaviour
{
    private int index;
    [SerializeField]
    private GameObject panelDialogue;
    private bool isTrigger;
    private void Start()
    {
        isTrigger = false;
    }
    private void Update()
    {
        if (isTrigger && (panelDialogue.activeSelf == false) && Input.GetKeyDown(KeyCode.Space))
        {
            panelDialogue.SetActive(true);
            BlockKeys.DialogOpened();
            GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Speed", 0);
            float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
            float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
            if (vertical != 0 && horizontal != 0)
            {
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
            }
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
