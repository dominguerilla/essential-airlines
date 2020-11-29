using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectLauncher : MonoBehaviour
{
    public GameObject launchPrefab;
    public float instantiateDistance = 1f;
    [SerializeField] float launchSpeed = 16f;

    InputActionAsset inputMap;

    [Header("Launcher parameters")]
    public Vector3 launchCameraPositionOffset;
    public Vector3 launchCameraLocalEulers;

    bool _isEngaged = false;
    Vector3 originalCameraOffset;
    Vector3 originalCameraLocalEulers;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        SetupInput();
    }

    public void Engage(Camera camera)
    {
        _isEngaged = true;
        cam = camera;
        originalCameraOffset = cam.transform.localPosition;
        originalCameraLocalEulers = cam.transform.localEulerAngles;
        cam.transform.localPosition = launchCameraPositionOffset;
        cam.transform.localEulerAngles = launchCameraLocalEulers;
        Debug.Log($"{ gameObject.name } LAUNCHER ENGAGED.");
    }

    public void DisEngage(Camera camera)
    {
        if (camera != cam)
        {
            throw new System.InvalidOperationException("Wrong camera being disengaged!");
        }
        _isEngaged = false;
        cam.transform.localPosition = originalCameraOffset;
        cam.transform.localEulerAngles = originalCameraLocalEulers;
        cam = null;
        Debug.Log($"{ gameObject.name } LAUNCHER DISENGAGED.");
    }

    public bool ToggleEngage(Camera camera)
    {
        if (_isEngaged)
        {
            DisEngage(camera);
        }
        else
        {
            Engage(camera);
        }

        return _isEngaged;
    }

    void SetupInput(){
        inputMap = GetComponent<PlayerInput>().actions;
        inputMap["Fire"].performed += Launch;
    }

    void SetupCamera()
    {
        
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
            drop.Launch(direction, launchSpeed);
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
