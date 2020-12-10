﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] PlayerInput player;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject startScreenUI;
    [SerializeField] Text startScreenTimer;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 cursorOffset;

    float startingPauseDuration = 5f;
    InputActionAsset inputMap;
    bool _isPaused = false;

    CursorLockMode _previousLockState;
    bool _wasCursorVisible;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) 
        {
            Debug.LogError($"Player not set on {gameObject.name}!");
            Destroy(this);
            return;
        }
        
        InitControls();
        InitLevel();
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

        _previousLockState = Cursor.lockState;
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

        Cursor.lockState = _previousLockState;
        Cursor.visible = _wasCursorVisible;

        Time.timeScale = 1;
        _isPaused = false;
    }

    void InitControls()
    {
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);
        inputMap = player.GetComponent<PlayerInput>().actions;
        inputMap["Pause"].performed += TogglePause;
    }

    void InitLevel()
    {
        player.DeactivateInput();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startScreenUI.SetActive(true);
        StartCoroutine(StartLevel(startingPauseDuration));
    }

    IEnumerator StartLevel(float pauseDuration)
    {
        float timePassed = 0f;
        while (timePassed < pauseDuration)
        {
            timePassed += Time.unscaledDeltaTime;
            var timeLeft = (decimal)(pauseDuration - timePassed);

            timeLeft = RoundNum(timeLeft);

            startScreenTimer.text =  timeLeft + "";
            yield return null;
        }
        player.ActivateInput();
        startScreenUI.SetActive(false);
    }

    decimal RoundNum(decimal num)
    {
        return decimal.Truncate(num * 100) / 100;
    }
}
