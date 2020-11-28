using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedObject : MonoBehaviour
{
    [SerializeField] float dropSpeed = 9.81f;

    bool _isDropping = false;

    // Start is called before the first frame update
    void Start()
    {
        Drop();
    }

    void Drop()
    {
        StartCoroutine(StartDrop(dropSpeed));
    }

    IEnumerator StartDrop(float dropSpeed)
    {
        _isDropping = true;
        while (_isDropping)
        {
            transform.position -= new Vector3(0, dropSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
