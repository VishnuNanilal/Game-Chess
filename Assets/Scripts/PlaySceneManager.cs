using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    static PlaySceneManager instance;

    public void SceneChanger(string newScene)
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        AsyncOperation ao = SceneManager.LoadSceneAsync(newScene);
    }

    public void QuitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
    public static PlaySceneManager Instance
    {
        get { return instance; }
    }
}
