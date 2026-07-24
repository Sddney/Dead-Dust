using UnityEngine;

public class PlayerHealthManagement : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = startingHealth;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        Debug.Log($"Player HP: {CurrentHealth}");
    }

    public void Damage(int amount)
    {
        CurrentHealth -= amount;

        Debug.Log($"Player HP: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}