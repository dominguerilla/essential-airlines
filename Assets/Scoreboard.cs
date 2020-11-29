using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] Text scoreText;
    int _currentScore = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void LogScore()
    {
        Debug.Log($"{gameObject.name} SCORE: {_currentScore}");
    }

    public void Score(int pointNum)
    {
        _currentScore += pointNum;
        UpdateUI();
    }

    void UpdateUI()
    {
        string txt = $"{_currentScore}";
        if (_currentScore < 10) txt = "0" + txt;
        scoreText.text = txt;
    }
}
