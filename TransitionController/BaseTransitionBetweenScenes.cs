using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseTransitionBetweenScenes : MonoBehaviour
{
    protected bool isTrigger;
    [SerializeField]
    protected string nameNextScene;

    protected virtual void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen)
        {
            SceneManager.LoadScene(nameNextScene);
            Debug.Log("Переход на другую сцену");
        }
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
