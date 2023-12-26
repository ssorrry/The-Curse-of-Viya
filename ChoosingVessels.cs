using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosingVessels: MonoBehaviour
{
    private List<Transform> buttonChildren = new List<Transform>();
    [SerializeField]
    private List<ItemScriptableObject> itemScrObj = new List<ItemScriptableObject>();
    private InventoryManager inventory;
    [SerializeField]
    private GameObject choosingTakeItem;
    [SerializeField]
    private GameObject buttons;
    private List<GameObject> buttonVessel = new List<GameObject>();
    private void Awake()
    {
        inventory = GameObject.FindWithTag("Canvas").GetComponent<InventoryManager>();
    }
    private void OnEnable()
    {
        buttonVessel.Clear();

        Transform buttonsTransform = buttons.GetComponent<Transform>();

        foreach (Transform child in buttonsTransform)
        {
            buttonVessel.Add(child.gameObject);
        }
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (!inventory.slots[i].isEmpty)
            {
                for (int j = 0; j < buttonVessel.Count; j++)
                {
                    SelectedVessel selectedVessel = buttonVessel[j].GetComponent<SelectedVessel>();
                    if (selectedVessel != null && selectedVessel.itemScrObj.itemName == inventory.slots[i].itemScrObj.itemName)
                    {
                        buttonVessel[j].SetActive(false);
                        buttonVessel.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
        Debug.Log(buttonVessel.Count);
        for (int i = 0; i < buttonVessel.Count; i++)
        {
            // Проверяем, что компонент присутствует и не скрыт
            if (buttonVessel[i].activeInHierarchy)
            {
                // Получаем компонент Selectable
                Selectable currentSelectable = buttonVessel[i].GetComponent<Selectable>();

                // Проверяем, что у компонента есть свойство Navigation
                if (currentSelectable != null && currentSelectable.navigation.mode == Navigation.Mode.Explicit)
                {
                    // Настройка навигации
                    Navigation nav = currentSelectable.navigation;

                    // Например, установка следующего элемента при нажатии вниз
                    if (i < buttonVessel.Count - 1)
                    {
                        nav.selectOnDown = buttonVessel[i + 1].GetComponent<Selectable>();
                    }
                    else
                    {
                        nav.selectOnDown = buttonVessel[0].GetComponent<Selectable>(); // Можете установить null, если вниз больше нет элементов
                    }

                    // Например, установка предыдущего элемента при нажатии вверх
                    if (i > 0)
                    {
                        nav.selectOnUp = buttonVessel[i - 1].GetComponent<Selectable>();
                    }
                    if (i == 0)
                    {
                        nav.selectOnUp = buttonVessel[buttonVessel.Count - 1].GetComponent<Selectable>(); // Можете установить null, если вверх больше нет элементов
                    }

                    // Применение изменений
                    currentSelectable.navigation = nav;
                }
            }
        }
        buttonVessel[0].GetComponent<Button>().Select();

        if (buttonVessel.Count == 1) 
        {
            gameObject.SetActive(false);
            BlockKeys.DialogClosed();
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            buttonVessel[0].GetComponent<Button>().Select();   
        }
    }
    public void IgnoreChoosingVessels() 
    { 
        WardrobeController.closePanelChoosingVessels = false;
        BlockKeys.DialogClosed();
    }
}
