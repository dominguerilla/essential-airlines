using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlyer : MonoBehaviour
{
    public float thrust = 10f;
    public float rollSpeed = 1f;
    InputActionAsset inputMap;
    
    // Start is called before the first frame update
    void Start()
    {
        SetupInput();
    }

    void SetupInput()
    {
        inputMap = GetComponent<PlayerInput>().actions;
        inputMap.Enable();


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.InverseTransformDirection(transform.forward) * Time.deltaTime * thrust);
        Vector2 moveDirection = inputMap["Move"].ReadValue<Vector2>();
        float rollDirection = inputMap["Roll"].ReadValue<float>();
        transform.Rotate(moveDirection.y, moveDirection.x, rollDirection * rollSpeed);
    }
}
