using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject pauseMenuUI;

    InputActionAsset inputMap;
    bool _isPaused = false;

    CursorLockMode previousLockState;
    bool _wasCursorVisible;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputMap = GetComponent<PlayerInput>().actions;
        inputMap["Pause"].performed += TogglePause;
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
}
