using System;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        _menuButton.onClick.AddListener(HandleMenuButtonClicked);
        _restartButton.onClick.AddListener(HandleRestartButtonClicked);
    }

    private void OnDestroy()
    {
        _menuButton.onClick.RemoveListener(HandleMenuButtonClicked);
        _restartButton.onClick.RemoveListener(HandleRestartButtonClicked);
    }

    private void HandleMenuButtonClicked()
    {
        _sceneLoader.LoadMenu();
    }

    private void HandleRestartButtonClicked()
    {
        _sceneLoader.RestartScene();
    }
}
