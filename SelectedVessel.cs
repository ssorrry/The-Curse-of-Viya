using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedVessel : MonoBehaviour
{
    [SerializeField]
    private GameObject choosingTakeItem;
    [SerializeField]
    public ItemScriptableObject itemScrObj;
    [SerializeField]
    private InventoryManager inventory;
    private bool thisVessel;
    private bool isWork;
    private void Start()
    {
        thisVessel = false;
        isWork = true;
        foreach (var slot in inventory.slots)
        {
            if (!slot.isEmpty && slot.itemScrObj.itemName == itemScrObj.itemName)
            {
                isWork = false;
            }
        }
    }
    private void OnEnable()
    {
        foreach (var slot in inventory.slots)
        {
            if (!slot.isEmpty && slot.itemScrObj.itemName == itemScrObj.itemName)
            {
                isWork = false;
            }
        }
    }
    private void Update()
    {
        if (!choosingTakeItem.activeInHierarchy && choosingTakeItem.GetComponent<ChoosingTakeItem>().isTake && thisVessel)
        {
            WardrobeController.closePanelChoosingVessels = false;
            inventory.AddItem(itemScrObj);
            SaveProgressScene();
            BlockKeys.DialogClosed();
            thisVessel = false;
            
        }
        if (!choosingTakeItem.activeInHierarchy && !choosingTakeItem.GetComponent<ChoosingTakeItem>().isTake && thisVessel)
        {
            WardrobeController.closePanelChoosingVessels = false;
            BlockKeys.DialogClosed();
            thisVessel = false;
        }
    }
    public void SelectedThisVessel()
    {
        if (isWork)
        {
            WardrobeController.closePanelChoosingVessels = false;
            choosingTakeItem.SetActive(true);
            choosingTakeItem.GetComponent<ChoosingTakeItem>().actionText.text = $"����� {itemScrObj.itemName}";
            thisVessel = true;
        }
        
    }

    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.sceneInnRoomLetterTaken = true;
        progress.itemNames.Add(itemScrObj.itemName);
        progress.nameFile = "/currentSession.dat";

        // ��������� ������� �������� �� �����
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // ��������� ������ �������� ���������
        currentProgress.itemNames.AddRange(progress.itemNames);

        // ��������� ����������� ������ � ����
        SaveLoadManager.SaveGameProgress(currentProgress);
    }
}
