using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Header("Debug settings")]
    public bool curScene = false;
    public bool nextScene = false;
    public bool prevScene = false;

    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void CurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if(curScene)
        {
            CurrentScene();
        }

        if(nextScene)
        {
            NextScene();
        }
        if(prevScene)
        {
            PreviousScene();
        }
    }
}
