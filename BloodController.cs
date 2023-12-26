using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    public bool isTrigger;
    [SerializeField]
    private GameObject sableDie;
    [SerializeField]
    private GameObject background;
    private bool hasDied = false;
    public bool sceneAtticEnemy;
    private void Update()
    {
        if (isTrigger && !hasDied && !sceneAtticEnemy) 
        {
            hasDied = true;
            Instantiate(background);
            Instantiate(sableDie);
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isDead = true;
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
