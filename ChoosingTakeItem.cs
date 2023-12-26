using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoosingTakeItem : MonoBehaviour
{
    public bool isTake = false;
    public TMP_Text actionText;
    [SerializeField]
    private Button buttonYes;

    private void OnEnable() 
    {
        buttonYes.Select();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            buttonYes.Select();
        }
    }
    public void TakeItem() 
    {
        isTake = true;
        gameObject.SetActive(false);
    }
    public void NoTakeItem()
    {
        isTake = false;
        gameObject.SetActive(false);
        BlockKeys.DialogClosed();
    }
}
