using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health;

    public void Initialize(EnemyData data)
    {
        health = data.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}