using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinUI : MonoBehaviour
{

    [SerializeField] private AudioClip _audioClip;

    [Header("Buttons")]
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;

    private SceneLoader _sceneLoader;
    private AudioManager _audioManager;

    private void Awake()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();
        _audioManager = FindAnyObjectByType<AudioManager>();

        _menuButton.onClick.AddListener(HandleMenuButtonClicked);
        _restartButton.onClick.AddListener(HandleRestartButtonClicked);
    }

    private void OnDestroy()
    {
        _menuButton.onClick.RemoveListener(HandleMenuButtonClicked);
        _restartButton.onClick.RemoveListener(HandleRestartButtonClicked);
    }

    private void HandleRestartButtonClicked()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        Time.timeScale = 1;
        _audioManager.PlaySound(_audioClip);
        yield return new WaitForSeconds(.5f);
        _sceneLoader.RestartScene();
    }

    private void HandleMenuButtonClicked()
    {
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        Time.timeScale = 1;
        _audioManager.PlaySound(_audioClip);
        yield return new WaitForSeconds(.5f);
        _sceneLoader.LoadMenu();
    }
    
}