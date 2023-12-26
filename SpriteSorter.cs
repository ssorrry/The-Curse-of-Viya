using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    public Transform character;  // Ссылка на трансформ персонажа
    public SpriteRenderer characterRenderer;  // Ссылка на SpriteRenderer персонажа
    public SpriteRenderer[] objectsToSort;  // Массив SpriteRenderer объектов, которые нужно сортировать
    public SpriteRenderer[] sameLevelObjects;  // Массив объектов, которые должны быть на том же уровне, что и персонаж

    void Update()
    {
        // Пройти по всем объектам, которые нужно сортировать
        foreach (SpriteRenderer objRenderer in objectsToSort)
        {
            // Сортировка только для видимых объектов
            if (objRenderer.isVisible)
            {
                // Проверка, находится ли персонаж перед объектом
                if (IsCharacterInFront(objRenderer.transform.position))
                {
                    // Если персонаж перед объектом, установить Order in Layer выше
                    objRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
                }
                else
                {
                    // Если персонаж за объектом, установить Order in Layer ниже
                    objRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
                }
            }
        }

        // Установить одинаковый Order in Layer для объектов на одном уровне с персонажем
        foreach (SpriteRenderer sameLevelObjRenderer in sameLevelObjects)
        {
            if (sameLevelObjRenderer.isVisible)
            {
                sameLevelObjRenderer.sortingOrder = 1;
            }
        }
    }

    // Функция для определения, находится ли персонаж перед объектом
    bool IsCharacterInFront(Vector3 objectPosition)
    {
        return character.position.y > objectPosition.y;
    }
}
