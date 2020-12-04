﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 cursorOffset;

    InputActionAsset inputMap;
    bool _isPaused = false;

    CursorLockMode previousLockState;
    bool _wasCursorVisible;

    // Start is called before the first frame update
    void Start()
    {
        if (playerObject == null) 
        {
            Debug.LogError($"Player not set on {gameObject.name}!");
            Destroy(this);
            return;
        }

        InitControls();
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (_isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0;

        previousLockState = Cursor.lockState;
        _wasCursorVisible = Cursor.visible;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void UnPause()
    {
        inGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);

        Cursor.lockState = previousLockState;
        Cursor.visible = _wasCursorVisible;

        Time.timeScale = 1;
        _isPaused = false;
    }

    void InitControls()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);

        inputMap = playerObject.GetComponent<PlayerInput>().actions;
        inputMap["Pause"].performed += TogglePause;
    }
}
