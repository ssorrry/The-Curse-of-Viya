using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHallHallway: BaseTransitionBetweenScenes
{
    private string nameFile = "/currentSession.dat";
    protected override void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            SceneHallwayController.nameFile = nameFile;
        }
        base.Update();

    }
}
