using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Header("Debug settings")]
    //Debug variables in order to test functionality without VR input
    public bool curScene = false;
    public bool nextScene = false;
    public bool prevScene = false;

    //Function that gets called by UI buttons and uses the current scene and buildindex to go towards the previous scene in the build
    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Function that gets called by UI buttons and uses the current scene and buildindex to load the current scene again
    public void CurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Function that gets called by UI buttons and uses the buildindex and current scene to progress to the next scene in the build
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
