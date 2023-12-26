using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/RightVesselScriptableObject")]
public class RightVesselScriptableObject : ItemScriptableObject
{
    private ActionWithWater actionWithWater;
    private bool sceneSecretRoomTakenWater;
    public ItemScriptableObject vesselWithWater;
    private InventoryManager inventoryManager;
    public override void InteractWithItem()
    {
        LoadProgressScene();
        if (!sceneSecretRoomTakenWater) 
        { 
            actionWithWater = FindObjectOfType<ActionWithWater>();
            inventoryManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InventoryManager>();
            if (actionWithWater != null && actionWithWater.isTrigger) 
            {
                inventoryManager.AddItem(vesselWithWater);
                SaveProgressScene();
                Debug.Log("��� �����, ������� ����");
                inventoryManager.RemoveItem(this);
            }
        }
        else
        {
            Debug.Log("�� ��� ������� ����");
        }
        
    }

    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.sceneSecretRoomTakenWater = true;
        progress.itemNames.Add(vesselWithWater.itemName);
        progress.itemNames.Remove(this.itemName);
        progress.nameFile = "/currentSession.dat";

        // ��������� ������� �������� �� �����
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // ��������� ������ �������� ���������
        currentProgress.sceneSecretRoomTakenWater = progress.sceneSecretRoomTakenWater;
        currentProgress.itemNames.AddRange(progress.itemNames);
        currentProgress.itemNames.Remove(this.itemName);

        // ��������� ����������� ������ � ����
        SaveLoadManager.SaveGameProgress(currentProgress);
        Debug.Log("��������� ������");
    }
    void LoadProgressScene()
    {
        GameProgress loadedProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        if (loadedProgress != null)
        {
            sceneSecretRoomTakenWater = loadedProgress.sceneSecretRoomTakenWater;
            Debug.Log("��������� ������");
        }
    }
}
