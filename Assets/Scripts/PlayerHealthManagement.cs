using System;
using UnityEngine;

public class PlayerHealthManagement : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    [SerializeField] AudioClip playerHurtSound;
    [SerializeField] AudioClip playerHealSound;

    private AudioManager audioManager;
    private UIManager UIManager;

    private bool hasShield = false;

    public int CurrentHealth { get; private set; }

    public event EventHandler PlayerDied;

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        if (audioManager is null)
        {
            Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");
        }


        UIManager = FindAnyObjectByType<UIManager>();
        if (UIManager is null)
        {
            Debug.LogError("UI Manager is missing.");
        }

        CurrentHealth = startingHealth;
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Heal amount cannot be negative.");
        }

        if (CurrentHealth >= 100)
        {
            return;
        }

        if (CurrentHealth + amount > 100)
        {
            amount = 100 - CurrentHealth;
        }

        UIManager.UpdateHealthBar(CurrentHealth, startingHealth);

        audioManager.PlaySound(playerHealSound);
        CurrentHealth += amount;
        Debug.Log($"Player HP: {CurrentHealth}");
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Damage amount cannot be negative.");
        }

        if (hasShield)
        {
            Debug.Log("Has shield");
            return;
        }

        audioManager.PlaySound(playerHurtSound);
        CurrentHealth -= amount;

        Debug.Log($"Player HP: {CurrentHealth}");
        UIManager.UpdateHealthBar(CurrentHealth, startingHealth);

        if (CurrentHealth <= 0)
        {
            UIManager.ShowDeathUI();
            gameObject.SetActive(false);
            PlayerDied?.Invoke(this, EventArgs.Empty);
            Debug.Log("Player Died");
        }
    }

    public void ActivateShield(bool isActive)
    {
        hasShield = isActive;
    }
}