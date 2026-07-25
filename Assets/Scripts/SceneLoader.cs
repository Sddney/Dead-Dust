using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MainMenu,
    Level
}

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneName.Level.ToString());
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
