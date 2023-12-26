using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderController : MonoBehaviour
{
    [SerializeField]
    private string nameNextScene;
    private bool isTrigger;
    private void Update()
    {
        if (isTrigger) 
        {
            SceneManager.LoadScene(nameNextScene);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
