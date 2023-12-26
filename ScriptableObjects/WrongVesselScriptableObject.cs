using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObject/ItemScriptableObject/WrongVesselScriptableObject")]
public class WrongVesselScriptableObject : ItemScriptableObject
{
    private ActionWithWater actionWithWater;
    private GameObject player;
    public GameObject backgroundDie;
    public GameObject dieDuck;
    public override void InteractWithItem()
    {
        actionWithWater = FindObjectOfType<ActionWithWater>();
        if (actionWithWater != null && actionWithWater.isTrigger)
        {
            Debug.Log("Смерть");
            BlockKeys.DialogOpened();
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().Dead();
            player.transform.position = new Vector3(0, 0, 0);
            Instantiate(backgroundDie);
            Instantiate(dieDuck);
        }
    }
}
