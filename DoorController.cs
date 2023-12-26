using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public bool isTrigger;
    public bool sceneHallDoorOpen;
    [SerializeField]
    private GameObject crest;
    private void Update()
    {
        if (isTrigger && sceneHallDoorOpen && Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene("Ladder");
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
    public void DoorOpen() 
    {
        crest.SetActive(true);
    }
}
