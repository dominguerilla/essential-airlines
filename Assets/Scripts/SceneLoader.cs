using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync(index));
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(index);
        while (!load.isDone)
        {
            yield return null;
        }
    }
}
