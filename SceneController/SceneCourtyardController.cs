using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCourtyardController: MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private GameObject npc;
    private bool scenePartCompleted = false;
    [SerializeField]
    private DialogManager dialogManager;
    [SerializeField]
    private GameObject panelDialog;
    private bool isTrigger;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().isScriptActive = false;
        player.GetComponent<MoveCharacter>().isScriptActive = true;
        npc.SetActive(true);
    }
    private void Update()
    {
        if (isTrigger && !dialogManager.isDialogEnd) 
        { 
            panelDialog.SetActive(true);
        }
        if (dialogManager.isDialogEnd && !scenePartCompleted) 
        {
            scenePartCompleted = true;
            SaveProgressScene();
            player.GetComponent<PlayerController>().isScriptActive = true;
            player.GetComponent<MoveCharacter>().isScriptActive = false;
            Debug.Log(scenePartCompleted);
        }
    }

    public void SaveProgressScene()
    {
        GameProgress progress = new GameProgress();
        progress.sceneCourtyardAcquaintance = scenePartCompleted;
        progress.nameFile = "/currentSession.dat";
        SaveLoadManager.SaveGameProgress(progress);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
