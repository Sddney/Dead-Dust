using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        _startButton.onClick.AddListener(HandleStartButtonClicked);
        _exitButton.onClick.AddListener(HandleExitButtonClicked);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(HandleStartButtonClicked);
        _exitButton.onClick.RemoveListener(HandleExitButtonClicked);
    }

    private void HandleStartButtonClicked()
    {
        _sceneLoader.LoadLevel();
    }

    private void HandleExitButtonClicked()
    {
        _sceneLoader.ExitGame();
    }
}
