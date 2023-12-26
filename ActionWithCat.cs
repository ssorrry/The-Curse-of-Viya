using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWithCat: MonoBehaviour
{
    public bool isTrigger;
    [SerializeField]
    private GameObject panelChoseAction;
    private GameObject player;
    [SerializeField]
    private GameObject panelDialog_1;
    [SerializeField]
    private GameObject panelDialog_2;
    [SerializeField]
    private GameObject panelDialog_3;
    [SerializeField]
    private GameObject backgroundDie;
    [SerializeField]
    private GameObject prefabCatDie;
    public bool sceneHallDialogueWithCat;
    public bool sceneHallGavenAwaySoot;
    private bool isOpen;
    [SerializeField]
    private GameObject bigCat;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (!sceneHallDialogueWithCat) 
        {
            if (!panelChoseAction.activeInHierarchy && !isOpen) 
            {
                if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !panelDialog_1.activeInHierarchy)
                {
                    BlockKeys.DialogOpened();
                    panelChoseAction.SetActive(true);
                    player.GetComponent<Animator>().SetFloat("Speed", 0);
                    float vertical = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Vertical");
                    float horizontal = GameObject.FindWithTag("Player").GetComponent<Animator>().GetFloat("Horizontal");
                    if (vertical != 0 && horizontal != 0)
                    {
                        GameObject.FindWithTag("Player").GetComponent<Animator>().SetFloat("Vertical", 0);
                    }
                    isOpen = true;
                }
            }
            if (!panelChoseAction.activeInHierarchy && isOpen) 
            {
                isOpen = false;
            }
            
        }
        else
        {
            if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !sceneHallGavenAwaySoot)
            {
                Debug.Log($"sceneHallGavenAwaySoot = {sceneHallGavenAwaySoot}");
                panelDialog_2.SetActive(true);
            }
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
    public void DialogueWithCat() 
    {
        panelChoseAction.SetActive(false);
        panelDialog_1.SetActive(true);      
    }
    public void DieCat() 
    {
        panelChoseAction.SetActive(false);
        player.transform.position = new Vector3(0, 0, 0);
        player.GetComponent<PlayerController>().Dead();
        Instantiate(backgroundDie);
        Instantiate(prefabCatDie);
    }
    public void IgnoreCat() 
    {
        panelChoseAction.SetActive(false);
        BlockKeys.DialogClosed();
    }
    public void Dialogue() 
    {
        sceneHallGavenAwaySoot = true;
        SaveProgressScene();
        panelDialog_3.SetActive(true);
        bigCat.SetActive(true);
        gameObject.SetActive(false);
    }
    private void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.sceneHallGavenAwaySoot = sceneHallGavenAwaySoot;
        progress.nameFile = "/currentSession.dat";

        // Загружаем текущий прогресс из файла
        GameProgress currentProgress = SaveLoadManager.LoadGameProgress("/currentSession.dat");

        // Обновляем данные текущего прогресса
        currentProgress.sceneHallGavenAwaySoot = progress.sceneHallGavenAwaySoot;

        // Сохраняем обновленные данные в файл
        SaveLoadManager.SaveGameProgress(currentProgress);
        Debug.Log("Сохранили данные");
    }
}
