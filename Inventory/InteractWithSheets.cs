using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithSheets: MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(gameObject);
            BlockKeys.DialogClosed();
        }
    }
}
