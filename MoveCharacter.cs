using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private Transform targetPosition;
    private bool isMoving;
    public bool isScriptActive;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartMoving();
    }
    void FixedUpdate()
    {
        if (isMoving && isScriptActive)
        {
            Vector3 direction = (targetPosition.position - transform.position).normalized;
            rb.velocity = direction * speed;

            if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
            {
                rb.velocity = Vector3.zero;
                isMoving = false;
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        animator.SetFloat("Horizontal", 1f);
        animator.SetFloat("Speed", 1f);
    }

}
