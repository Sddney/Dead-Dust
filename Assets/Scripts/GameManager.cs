using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    private void Start()
    {
        _player.PlayerHealthManagement.PlayerDied += HandlePlayerDied;
    }

    private void HandlePlayerDied(object sender, System.EventArgs e)
    {
        _enemySpawner.StopSpawning();
        _enemySpawner.StopAllEnemies();
    }
}
