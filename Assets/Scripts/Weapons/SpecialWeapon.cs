using UnityEngine;

public class SpecialWeapon : Weapon
{
    [SerializeField] private float _shieldDuration = 3;

    public float ShieldDuration => _shieldDuration;

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, WeaponData.AttackDistance);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(AttackDamage);
            }

            if (collider.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.Knockback(WeaponData.KnockbackForce);
            }
        }
    }
}
