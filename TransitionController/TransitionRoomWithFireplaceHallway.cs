using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRoomWithFireplaceHallway: BaseTransitionBetweenScenes
{
    private string nameFile = "/currentSession.dat";
    protected override void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen)
        {
            SceneHallwayController.nameFile = nameFile;
        }
        base.Update();

    }
}
