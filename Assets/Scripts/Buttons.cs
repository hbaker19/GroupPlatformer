using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    private float timeScale;

    private void Awake()
    {
        timeScale = Time.timeScale;
    }
    public void ChangeScene()
    {
        Time.timeScale = timeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = timeScale;
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Pause()
    {
        if(Time.timeScale != 0)
        {
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = timeScale;
        }
    }
}
