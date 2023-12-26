using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/CrossScriptableObject")]
public class CrossScriptableObject: ItemScriptableObject
{
    private DoorController doorController;
    private InventoryManager inventoryManager;
    public override void InteractWithItem()
    {
        doorController = FindObjectOfType<DoorController>();
        inventoryManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InventoryManager>();

        if (doorController != null && doorController.isTrigger)
        {
            SaveProgressScene();
            doorController.DoorOpen();
            SceneManager.LoadScene("Ladder");
            inventoryManager.RemoveItem(this);
        }
    }
    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.itemNames.Remove(this.itemName);
        progress.sceneHallDoorOpen = true;
        progress.nameFile = "/currentSession.dat";

        // ��������� ������� �������� �� �����
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // ��������� ������ �������� ���������
        currentProgress.itemNames.Remove(this.itemName);
        currentProgress.sceneHallDoorOpen = progress.sceneHallDoorOpen;

        // ��������� ����������� ������ � ����
        SaveLoadManager.SaveGameProgress(currentProgress);
        Debug.Log("��������� ������");
    }
}
