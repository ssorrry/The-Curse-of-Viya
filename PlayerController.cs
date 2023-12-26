using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController: MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 direction;
    public bool isScriptActive;
    public bool isDead;
    private bool hasDied = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDead = false;
    }

    void FixedUpdate() 
    {
        if (!isDead)
        {
            if (!BlockKeys.dialogOpen && !BlockKeys.inventoryOpen && isScriptActive)
            {
                MovementLogic();
            }
        }
        else if(!hasDied)
        {
            hasDied = true;
            Dead();
        }
    }

    private void MovementLogic() 
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        Vector2 movement = new Vector2(direction.x, direction.y);
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }
    public void Dead() 
    {
        StartCoroutine(Die());
    }
    IEnumerator Die() 
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Player died");
        DeleteFile("/currentSession.dat");
        SceneManager.LoadScene("MainMenu");
        BlockKeys.DialogOpened();
    }
    private void DeleteFile(string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Файл удален: " + fileName);
        }
        else
        {
            Debug.LogWarning("Файл не существует: " + fileName);
        }
    }
}
