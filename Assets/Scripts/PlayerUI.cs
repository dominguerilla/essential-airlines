using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] float gameDuration = 60f;
    [SerializeField] float startingPauseDuration = 3f;

    [Header("Scene")]
    [SerializeField] PlayerInput player;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject startScreenUI;
    [SerializeField] GameObject endScreenUI;
    [SerializeField] Text startScreenTimer;
    [SerializeField] Text gameTimer;
    [SerializeField] Text finalScoreText;

    [Header("Cursor")]
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 cursorOffset;

    InputActionAsset inputMap;
    Scoreboard playerScoreboard;
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

        playerScoreboard = player.GetComponent<Scoreboard>();
        if (!playerScoreboard)
        {
            Debug.LogError($"Scoreboard not found on { player.gameObject.name }!");
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
        inputMap = player.actions;
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
        yield return StartTimer(pauseDuration, startScreenTimer, unscaled: true);
        player.ActivateInput();
        startScreenUI.SetActive(false);

        yield return StartGameClock();

        EndLevel();
    }

    void EndLevel()
    {
        Time.timeScale = 0;
        player.DeactivateInput();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        inGameUI.SetActive(false);

        int score;
        if (playerScoreboard)
        {
            score = playerScoreboard.GetScore();
        }
        else
        {
            score = -1;
        }

        InitEndScreen(score);
    }

    void InitEndScreen(int finalScore)
    {
        endScreenUI.SetActive(true);
        finalScoreText.text += finalScore + "";
    }

    IEnumerator StartTimer(float duration, Text timer, bool unscaled=false)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            float delta = unscaled ? Time.unscaledDeltaTime : Time.deltaTime;
            timePassed += delta;
            var timeLeft = (decimal)(duration - timePassed);

            timeLeft = RoundNum(timeLeft);

            timer.text = timeLeft + "";
            yield return null;
        }
        yield return null;
    }

    IEnumerator StartGameClock()
    {
        yield return StartTimer(gameDuration, gameTimer);
    }

    decimal RoundNum(decimal num)
    {
        return decimal.Truncate(num * 100) / 100;
    }
}
