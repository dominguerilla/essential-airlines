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
        
    }

    void Drop()
    {
        StartCoroutine(StartDrop(dropSpeed));
    }

    public void Launch(Vector3 direction, float launchSpeed)
    {
        StartCoroutine(StartLaunch(direction, launchSpeed));
    }

    IEnumerator StartLaunch(Vector3 direction, float launchSpeed)
    {
        _isDropping = true;
        while (_isDropping)
        {
            transform.position += direction * launchSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
