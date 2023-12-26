using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    private Transform playerTransform;
    
    [SerializeField]
    private float rightLimit, leftLimit, topLimit, bottomLimit;
    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.transform.position.x,leftLimit,rightLimit), 
            Mathf.Clamp(playerTransform.transform.position.y, topLimit, bottomLimit), 
            transform.position.z);
    }
}
