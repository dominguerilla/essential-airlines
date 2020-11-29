using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5f;

    bool _isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        _isRotating = true;
        while (_isRotating)
        {
            transform.Rotate(0, 0, rotateSpeed);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerFlyer player = other.GetComponentInParent<PlayerFlyer>();
        if (player)
        {
            player.AddAmmo(1);
            Destroy(this.gameObject);
        }
    }
}
