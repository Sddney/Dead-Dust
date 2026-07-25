using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private float health;

    AudioManager AudioManager;
    AudioClip hurtSound;

    void Start()
    {
        AudioManager ??= FindAnyObjectByType<AudioManager>();
        if (AudioManager is null)
        {
            Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");
        }
    }

    public void Initialize(EnemyData data)
    {
        health = data.maxHealth;
        hurtSound = data.hurtSound;

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
        Destroy(gameObject);
    }
}