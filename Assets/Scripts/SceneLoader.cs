﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(1);
        while (!load.isDone)
        {
            yield return null;
        }
    }
}
