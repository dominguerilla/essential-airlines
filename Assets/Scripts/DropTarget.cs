using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
    Scoreboard scoreboard;

    private void Start()
    {
        scoreboard = GameObject.FindObjectOfType<Scoreboard>();
    }
    public void Collect(DroppedObject drop)
    {
        Debug.Log($"Collected { drop.gameObject.name }!");
        Destroy(drop.gameObject);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        DroppedObject drop = other.GetComponent<DroppedObject>();
        if (drop)
        {
            Collect(drop);
            if (scoreboard)
            {
                scoreboard.Score(1);
            }
        }
    }
}
