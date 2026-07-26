using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private SceneLoader _sceneLoader;
    private AudioManager _audioManager;

    private void Awake()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();
        _audioManager = FindAnyObjectByType<AudioManager>();

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
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        _audioManager.PlaySound(_audioClip);
        yield return new WaitForSeconds(.5f);
        _sceneLoader.LoadLevel();
    }

    private void HandleExitButtonClicked()
    {
        StartCoroutine(ExitLevel());
    }

    private IEnumerator ExitLevel()
    {
        _audioManager.PlaySound(_audioClip);
        yield return new WaitForSeconds(.5f);
        _sceneLoader.ExitGame();
    }
}
