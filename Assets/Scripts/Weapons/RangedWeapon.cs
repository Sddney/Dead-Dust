using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPosition;

    [SerializeField] private float _projectileSpeed = 5f;
    
    private float ProjectileDistance => WeaponData.AttackDistance;

    private void Awake()
    {
        if (_projectilePrefab is null)
        {
            Debug.LogError($"Projectile Prefab is not assigned in {gameObject.name}");
        }

        if (_projectileSpawnPosition is null)
        {
            Debug.LogError($"Projectile Spawn Position is not assigned in {gameObject.name}");
        }
    }

    public override void Attack()
    {
        if (!CanShoot)
        {
            return;
        }

        AudioManager.PlaySound(WeaponData.Sound);
        var projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, _projectileSpawnPosition.rotation);
        projectile.SetAttackDamage(AttackDamage);
        projectile.SetSpeedAndDistance(_projectileSpeed, ProjectileDistance);

        ResetCooldown();
    }
}
