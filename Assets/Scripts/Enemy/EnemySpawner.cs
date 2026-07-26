using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private EnemyController normalEnemy;
    [SerializeField] private EnemyController tankEnemy;
    [SerializeField] private EnemyController rangedEnemy;

    [Header("Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay;

    [Header("Count")]
    public int normalCount;
    public int tankCount;
    public int rangedCount;

    public int TotalEnemies { get; private set; }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        List<EnemyController> enemiesToSpawn = new List<EnemyController>();

        normalCount = Random.Range(5, 15);
        tankCount = Random.Range(1, 8);
        rangedCount = Random.Range(5, 15);

        TotalEnemies = normalCount + tankCount + rangedCount;

        for (int i = 0; i < normalCount; i++)
            enemiesToSpawn.Add(normalEnemy);

        for (int i = 0; i < tankCount; i++)
            enemiesToSpawn.Add(tankEnemy);

        for (int i = 0; i < rangedCount; i++)
            enemiesToSpawn.Add(rangedEnemy);


        for (int i = enemiesToSpawn.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            EnemyController temp = enemiesToSpawn[i];
            enemiesToSpawn[i] = enemiesToSpawn[randomIndex];
            enemiesToSpawn[randomIndex] = temp;
        }


        foreach (EnemyController enemy in enemiesToSpawn)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(enemy, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void StopAllEnemies()
    {
        List<EnemyMovement> enemyMovements = FindObjectsByType<EnemyMovement>().ToList();

        foreach (EnemyMovement enemyMovement in enemyMovements)
        {
            enemyMovement.DisableMovement();
        }
    }
}
