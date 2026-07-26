using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MainMenu,
    Level
}

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

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
