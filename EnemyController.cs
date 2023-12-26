using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform target;
    private GameObject player;
    [SerializeField]
    private Animator animator;
    private bool isTrigger;
    private bool isDied = false;
    [SerializeField]
    private GameObject backgroundDie;
    [SerializeField]
    private GameObject enemyDie;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
    }
    private void Update()
    {
        if (isTrigger && !isDied) 
        {
            isDied = true;
            Debug.Log(isDied);
            player.GetComponent<PlayerController>().Dead();
            BlockKeys.DialogOpened();
            player.transform.position = new Vector3(0, 0, 0);
            Instantiate(backgroundDie);
            Instantiate(enemyDie);
        }
    }
    private void FixedUpdate()
    {
        if (!isDied) 
        { 
            float horizontalMovement = target.position.x - transform.position.x;
            float verticalMovement = target.position.y - transform.position.y;

            Vector2 movement = new Vector2(horizontalMovement, verticalMovement).normalized;
            animator.SetFloat("Speed",speed);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
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
