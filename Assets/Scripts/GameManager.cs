using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        _player.PlayerHealthManagement.PlayerDied += HandlePlayerDied;
    }

    private void HandlePlayerDied(object sender, System.EventArgs e)
    {
        Time.timeScale = 0;
    }
}
