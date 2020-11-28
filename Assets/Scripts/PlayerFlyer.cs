using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlyer : MonoBehaviour
{
    public float thrust = 10f;
    public float rollSpeed = 1f;
    [SerializeField] int thrustBonus = 0;
    InputActionAsset inputMap;

    int thrustFactor = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetupInput();
    }

    void SetupInput()
    {
        inputMap = GetComponent<PlayerInput>().actions;
        inputMap["Accelerate"].performed += BoostThrust;
        inputMap["Decelerate"].performed += ResetThrust;
    }

    void BoostThrust(InputAction.CallbackContext context)
    {
        thrustFactor = 1 + thrustBonus;
    }

    void ResetThrust(InputAction.CallbackContext context)
    {
        thrustFactor = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.InverseTransformDirection(transform.forward) * Time.deltaTime * thrust * thrustFactor);
        Vector2 moveDirection = inputMap["Move"].ReadValue<Vector2>();
        float rollDirection = inputMap["Roll"].ReadValue<float>();
        transform.Rotate(moveDirection.y, moveDirection.x, rollDirection * rollSpeed);
    }
}
