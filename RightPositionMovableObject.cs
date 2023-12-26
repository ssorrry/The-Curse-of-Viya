using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPositionMovableObject: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            TableInnRoomController.objectMoved = true;
        }
    }
}
