using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private EnemyController normalEnemy;
    [SerializeField] private EnemyController tankEnemy;
    [SerializeField] private EnemyController rangedEnemy;

    [Header("Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay = 1f;

    [SerializeField] private int normalCount = 5;
    [SerializeField] private int tankCount = 1;
    [SerializeField] private int rangedCount = 2;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return SpawnEnemies(normalEnemy, normalCount);
            yield return SpawnEnemies(tankEnemy, tankCount);
            yield return SpawnEnemies(rangedEnemy, rangedCount);
        }
    }

    private IEnumerator SpawnEnemies(EnemyController enemyPrefab, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}