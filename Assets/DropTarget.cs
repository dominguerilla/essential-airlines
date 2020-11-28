using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
    public void Collect(DroppedObject drop)
    {
        Debug.Log($"Collected { drop.gameObject.name }!");
        Destroy(drop.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        DroppedObject drop = other.GetComponent<DroppedObject>();
        if (drop)
        {
            Collect(drop);
        }
    }
}
