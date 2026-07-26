using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private EnemyData enemyData;
    private float health;

    private AudioManager AudioManager;
    private AudioClip hurtSound;

    private PointsManager PointsManager;

    void Start()
    {
        AudioManager ??= FindAnyObjectByType<AudioManager>();
        if (AudioManager is null)
            Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");

        PointsManager ??= FindAnyObjectByType<PointsManager>();
        if (PointsManager is null)
            Debug.LogError("PointsManager not found in the scene. Please make sure there is a PointsManager in the scene.");
    }

    public void Initialize(EnemyData data)
    {
        health = data.maxHealth;
        hurtSound = data.hurtSound;
        enemyData = data;
    }

    public void TakeDamage(float damage)
    {
        AudioManager.PlaySound(hurtSound);
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        PointsManager.AddKilled(enemyData.enemyType);
        Destroy(gameObject);
    }
}
