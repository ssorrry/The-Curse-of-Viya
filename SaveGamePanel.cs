using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGamePanel : MonoBehaviour
{
    [SerializeField]
    private Button button;
    private void OnEnable()
    {
        button.Select();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            button.Select();
        }
    }
}
