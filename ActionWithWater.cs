using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionWithWater : MonoBehaviour
{
    public bool isTrigger;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private GameObject prefabButton;
    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private GameObject panelActionWater;

    private void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space)) 
        {
            //� ����������� �� ����, ��� ����� � ��������� (������)
            //������� ������ (��������� ���� � �������)
            //��� ����� ������� "������������ + �������� ������"
            //�� ������ �����, ������� �������� �������� ������� ����������
            //���� true, �� �������� ����
            //���� false, �� ������
           
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
