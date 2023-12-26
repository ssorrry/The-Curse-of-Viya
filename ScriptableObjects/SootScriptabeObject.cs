using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/SootScriptabeObject")]
public class SootScriptabeObject: ItemScriptableObject
{
    private ActionWithCat actionWithCat;
    private InventoryManager inventoryManager;
    public ItemScriptableObject falseCross;
    public override void InteractWithItem()
    {
        actionWithCat = FindObjectOfType<ActionWithCat>();
        inventoryManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InventoryManager>();
        if (actionWithCat != null && actionWithCat.isTrigger)
        {
            actionWithCat.Dialogue();
            inventoryManager.AddItem(falseCross);
            SaveProgressScene();
            inventoryManager.RemoveItem(this);
        }
    }
    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.itemNames.Add(falseCross.itemName);
        progress.itemNames.Remove(this.itemName);
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.itemNames.AddRange(progress.itemNames);
        currentProgress.itemNames.Remove(this.itemName);

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
        Debug.Log("Сохранили данные");
    }
}
