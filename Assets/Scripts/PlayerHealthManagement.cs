using System;
using UnityEngine;

public class PlayerHealthManagement : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 100;

    [SerializeField] private AudioClip _playerHurtSound;
    [SerializeField] private AudioClip _playerHealSound;

    private AudioManager _audioManager;
    private UIManager _uiManager;

    private bool _hasShield = false;

    public int CurrentHealth { get; private set; }

    public bool IsFullHealth => CurrentHealth == _startingHealth;

    public event EventHandler PlayerDied;

    VFXManager VFXManager;

    private void Awake()
    {
        _audioManager = FindAnyObjectByType<AudioManager>();
        if (_audioManager is null)
        {
            Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");
        }


        _uiManager = FindAnyObjectByType<UIManager>();
        if (_uiManager is null)
        {
            Debug.LogError("UI Manager is missing.");
        }

        CurrentHealth = _startingHealth;

        VFXManager = FindAnyObjectByType<VFXManager>();
        if(VFXManager is null) Debug.LogError("VFX Manager is missing.");
        
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

        _audioManager.PlaySound(_playerHealSound);
        CurrentHealth += amount;
        _uiManager.UpdateHealthBar(CurrentHealth, _startingHealth);
        Debug.Log($"Player HP: {CurrentHealth}");
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Damage amount cannot be negative.");
        }

        if (_hasShield)
        {
            Debug.Log("Has shield");
            return;
        }

        _audioManager.PlaySound(_playerHurtSound);
        CurrentHealth -= amount;
        VFXManager.PlayerDamagePlay();

        Debug.Log($"Player HP: {CurrentHealth}");
        _uiManager.UpdateHealthBar(CurrentHealth, _startingHealth);

        if (CurrentHealth <= 0)
        {
            _uiManager.ShowDeathUI();
            gameObject.SetActive(false);
            PlayerDied?.Invoke(this, EventArgs.Empty);
            Debug.Log("Player Died");
        }
    }

    public void ActivateShield(bool isActive)
    {
        _hasShield = isActive;
    }
}