using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropTarget : MonoBehaviour
{
    [SerializeField] Material enabledMaterial;
    [SerializeField] Material disabledMaterial;
    [SerializeField] Renderer[] renderers;
    [SerializeField] UnityEvent onCollect = new UnityEvent();

    Scoreboard scoreboard;

    bool _isEnabled = true;

    private void Start()
    {
        scoreboard = GameObject.FindObjectOfType<Scoreboard>();
        if (gameObject.activeSelf)
        {
            EnableTarget();
        }
        else
        {
            DisableTarget();
        }
    }

    public void Collect(DroppedObject drop)
    {
        Destroy(drop.gameObject);
        onCollect.Invoke();

        DisableTarget();
    }

    public void EnableTarget()
    {
        _isEnabled = true;
        foreach (Renderer render in renderers)
        {
            render.material = enabledMaterial;
        }
    }

    public void DisableTarget()
    {
        _isEnabled = false;
        foreach (Renderer render in renderers)
        {
            render.material = disabledMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isEnabled) return;
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
