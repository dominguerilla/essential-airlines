﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(ObjectLauncher))]
public class PlayerFlyer : MonoBehaviour
{
    public float thrust = 10f;
    public float rollSpeed = 1f;
    [SerializeField] int thrustBonus = 0;

    InputActionAsset inputMap;
    ObjectLauncher launcher;
    int thrustFactor = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetupInput();
        launcher = GetComponent<ObjectLauncher>();
    }

    void SetupInput()
    {
        inputMap = GetComponent<PlayerInput>().actions;
        inputMap["Accelerate"].performed += BoostThrust;
        inputMap["Decelerate"].performed += ResetThrust;
        inputMap["Toggle Engage"].performed += ToggleLauncherEngage;
    }

    void BoostThrust(InputAction.CallbackContext context)
    {
        thrustFactor = 1 + thrustBonus;
    }

    void ResetThrust(InputAction.CallbackContext context)
    {
        thrustFactor = 1;
    }

    void ToggleLauncherEngage(InputAction.CallbackContext context)
    {
        launcher.ToggleEngage();
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