using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    public Transform character;  // ������ �� ��������� ���������
    public SpriteRenderer characterRenderer;  // ������ �� SpriteRenderer ���������
    public SpriteRenderer[] objectsToSort;  // ������ SpriteRenderer ��������, ������� ����� �����������
    public SpriteRenderer[] sameLevelObjects;  // ������ ��������, ������� ������ ���� �� ��� �� ������, ��� � ��������

    void Update()
    {
        // ������ �� ���� ��������, ������� ����� �����������
        foreach (SpriteRenderer objRenderer in objectsToSort)
        {
            // ���������� ������ ��� ������� ��������
            if (objRenderer.isVisible)
            {
                // ��������, ��������� �� �������� ����� ��������
                if (IsCharacterInFront(objRenderer.transform.position))
                {
                    // ���� �������� ����� ��������, ���������� Order in Layer ����
                    objRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
                }
                else
                {
                    // ���� �������� �� ��������, ���������� Order in Layer ����
                    objRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
                }
            }
        }

        // ���������� ���������� Order in Layer ��� �������� �� ����� ������ � ����������
        foreach (SpriteRenderer sameLevelObjRenderer in sameLevelObjects)
        {
            if (sameLevelObjRenderer.isVisible)
            {
                sameLevelObjRenderer.sortingOrder = 1;
            }
        }
    }

    // ������� ��� �����������, ��������� �� �������� ����� ��������
    bool IsCharacterInFront(Vector3 objectPosition)
    {
        return character.position.y > objectPosition.y;
    }
}
