using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageSecretRoomController : MonoBehaviour
{
    private bool isTrigger;
    public static bool sceneHallInfoSecretRoom;
    public static bool sceneSecretRoomOpen;
    [SerializeField]
    private GameObject passage;
    [SerializeField]
    private GameObject doorToSecretRoom;
    private bool isOpen;
    private void Start()
    {
        if (sceneSecretRoomOpen) 
        {
            passage.SetActive(false);
            doorToSecretRoom.SetActive(true);
        }
        else
        {
            passage.SetActive(true);
            doorToSecretRoom.SetActive(false);
        }
    }
    void Update()
    {
        if (!sceneSecretRoomOpen) 
        {
            if (sceneHallInfoSecretRoom && isTrigger && !isOpen && Input.GetKeyDown(KeyCode.Space))
            {
                passage.SetActive(false);
                doorToSecretRoom.SetActive(true);
                isOpen = true;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
