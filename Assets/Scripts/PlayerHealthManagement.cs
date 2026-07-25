using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManagement : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    public int CurrentHealth { get; private set; }

    AudioManager audioManager;
    [SerializeField] AudioClip playerHurtSound;
    [SerializeField] AudioClip playerHealSound;
    UIManager UIManager;

    private void Awake()
    {
        audioManager ??= FindAnyObjectByType<AudioManager>();
        if (audioManager is null) Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");
        

        UIManager ??= FindAnyObjectByType<UIManager>();
        if (!UIManager) Debug.LogError("UI Manager is missing.");
        CurrentHealth = startingHealth;
    }

    public void Heal(int amount)
    {
        if (CurrentHealth >= 100) return;
        
        if (CurrentHealth + amount > 100) amount = 100 - CurrentHealth;
        UIManager.UpdateHealthBar(CurrentHealth, startingHealth);

        audioManager.PlaySound(playerHealSound);
        CurrentHealth += amount;
        Debug.Log($"Player HP: {CurrentHealth}");
    }

    public void Damage(int amount)
    {
        audioManager.PlaySound(playerHurtSound);
        CurrentHealth -= amount;

        Debug.Log($"Player HP: {CurrentHealth}");
        UIManager.UpdateHealthBar(CurrentHealth, startingHealth);

        if (CurrentHealth <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
    }
}