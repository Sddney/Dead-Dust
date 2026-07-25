using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData WeaponData;

    private bool _canShoot = true;
    private float _currentCooldown = 0.0f;

    protected AudioManager AudioManager;

    public bool CanShoot => _canShoot;
    public float AttackDamage => WeaponData.AttackDamage;
    
    public float Cooldown => WeaponData.Cooldown;

    private void OnEnable()
    {
        AudioManager ??= FindAnyObjectByType<AudioManager>();
        if (AudioManager is null)
        {
            Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");
        }
    }

    private void Update()
    {
        _currentCooldown += Time.deltaTime;
        if (_currentCooldown >= WeaponData.Cooldown)
        {
            _canShoot = true;
        }
    }

    public void ResetCooldown()
    {
        _canShoot = false;
        _currentCooldown = 0;
    }

    public abstract void Attack();
}
