using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionInnRoomHallway: BaseTransitionBetweenScenes
{
    private string nameFile = "/currentSession.dat";
    protected override void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen)
        {
            Debug.Log("Переход в коридор");
            SceneHallwayController.nameFile = nameFile;
        }
        base.Update();
    }
}
