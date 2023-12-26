using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseController: MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public bool isTrigger;
    [SerializeField]
    private GameObject bookcaseDie;
    [SerializeField]
    private GameObject background;
    private bool hasDied = false;
    private void Update()
    {
        if (isTrigger && !hasDied)
        {
            hasDied = true;
            Instantiate(background);
            Instantiate(bookcaseDie);
            player.transform.position = new Vector3(0, 0, 0);
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
