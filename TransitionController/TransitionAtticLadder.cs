using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAtticLadder: BaseTransitionBetweenScenes
{
    protected override void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space) && !BlockKeys.dialogOpen && !BlockKeys.inventoryOpen)
        {
            SceneLadderController.transitionFromAttic = true;
            SceneManager.LoadScene(nameNextScene);
        }
    }
}
