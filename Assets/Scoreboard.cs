using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    int _currentScore = 0;

    private void Start()
    {
        LogScore();
    }

    public void LogScore()
    {
        Debug.Log($"{gameObject.name} SCORE: {_currentScore}");
    }

    public void Score(int pointNum)
    {
        _currentScore += pointNum;
    }
}
