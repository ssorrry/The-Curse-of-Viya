using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBook: ActionWithBook
{
    [SerializeField]
    private GameObject choosingTakeItem;
    public ItemScriptableObject itemScrObj;
    private bool isTaken;
    [SerializeField]
    private GameObject backgroundDie;
    [SerializeField]
    private GameObject bookDie;
    private GameObject player;
    public bool sceneHallTakenCross;
    [SerializeField]
    private GameObject bibliya;
    [SerializeField]
    private Sprite bibliyaCross;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void Update()
    {
        base.Update();
        if (!choosingTakeItem.activeInHierarchy && choosingTakeItem.GetComponent<ChoosingTakeItem>().isTake && !isTaken && !sceneHallTakenCross) 
        {
            GameObject.FindWithTag("Canvas").GetComponent<InventoryManager>().AddItem(itemScrObj);
            isEnd = false;
            Debug.Log("положили крест в инвентарь");
            isTaken = true;
            StartCoroutine(DieBook());
            BlockKeys.DialogClosed();
            bookPanel.SetActive(false);
        }
        if (!choosingTakeItem.activeInHierarchy && !choosingTakeItem.GetComponent<ChoosingTakeItem>().isTake && !isTaken && !isEnd && !sceneHallTakenCross) 
        {
            isEnd = true;
        }

        
    }
    protected override void ActionBook()
    {
        base.ActionBook();
        bookPanel.SetActive(false);
        choosingTakeItem.SetActive(true);
        choosingTakeItem.GetComponent<ChoosingTakeItem>().actionText.text = $"Взять предмет: {itemScrObj.itemName}?";
    }
    protected override void IgnoreBook()
    {
        base.IgnoreBook();
        if (!isTaken)
        {
            isEnd = true;
        }
        else
        {
            isEnd = false;
        }
    }
    IEnumerator DieBook() 
    {
        yield return new WaitForSeconds(2f);
        BlockKeys.DialogOpened();
        player.GetComponent<PlayerController>().Dead();
        player.transform.position = new Vector3(0,0,0);
        Instantiate(backgroundDie);
        Instantiate(bookDie);
    }
    public void TakeCross() 
    {
        bibliya.GetComponent<SpriteRenderer>().sprite = bibliyaCross;
    }
}
