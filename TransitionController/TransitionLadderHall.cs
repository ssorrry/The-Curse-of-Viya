using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLadderHall: BaseTransitionBetweenScenes
{
    protected override void Update() 
    {
        if (isTrigger && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen)
        {
            SceneHallController.transitionFromLadder = true;
            SceneManager.LoadScene(nameNextScene);
        }
    }
}
