using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectLauncher : MonoBehaviour
{
    public GameObject launchPrefab;
    public float instantiateDistance = 1f;
    InputActionAsset inputMap;

    bool _isEngaged = false;

    // Start is called before the first frame update
    void Start()
    {
        SetupInput();
        Engage();
    }

    public void Engage()
    {
        _isEngaged = true;
        Debug.Log($"{ gameObject.name } LAUNCHER ENGAGED.");
    }

    public void DisEngage()
    {
        _isEngaged = false;
        Debug.Log($"{ gameObject.name } LAUNCHER DISENGAGED.");
    }

    void SetupInput(){
        inputMap = GetComponent<PlayerInput>().actions;
        inputMap["Fire"].performed += Launch;
    }

    public void Launch(InputAction.CallbackContext context)
    {
        if (_isEngaged)
        {
            Vector3 launchVector = GetLaunchVector(context);
            LaunchObject(launchVector);
            Debug.Log("Launch!");
        }
        
    }

    void LaunchObject(Vector3 direction)
    {
        GameObject launchedObject = GameObject.Instantiate(launchPrefab, transform.position + direction, launchPrefab.transform.rotation);
        DroppedObject drop = launchedObject.GetComponent<DroppedObject>();
        if (drop)
        {
            drop.Launch(direction);
        }
    }


    Vector3 GetLaunchVector(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseVector3 = new Vector3(mousePosition.x, mousePosition.y);
        Ray ray = Camera.main.ScreenPointToRay(mouseVector3);
        return ray.direction;
    }
}
