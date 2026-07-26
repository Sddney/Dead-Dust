using System.Collections;
using UnityEngine;

public class SpecialWeapon : Weapon
{
    [SerializeField] private float _shieldDuration = 3;
    [SerializeField] private float _blanketActiveDuration = 0.25f;
    [SerializeField] private GameObject _blanket;

    public float ShieldDuration => _shieldDuration;
    public float BlanketActiveDuration => _blanketActiveDuration;

    public override void Attack()
    {
        StartCoroutine(DealAttack());
    }

    private IEnumerator DealAttack()
    {
        Vector2 size = new(WeaponData.AttackDistance, WeaponData.AttackDistance);
        var blanket = Instantiate(_blanket, transform.position, transform.rotation);
        blanket.transform.localScale = size;

        AudioManager.PlaySound(WeaponData.Sound);
        yield return new WaitForSeconds(_blanketActiveDuration);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);

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

        Destroy(blanket);
    }
}
