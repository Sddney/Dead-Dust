using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float _leftSwingAngle = -60f;
    [SerializeField] private float _rightSwingAngle = 60f;
    [SerializeField, Range(0f, 360f)] private float _attackAngle = 180f;

    [SerializeField] private float _attackRadius = 2.5f;
    [SerializeField] private float _swingDuration = 0.15f;

    private bool _isAttacking;
    private Quaternion _defaultRotation;

    private void Awake()
    {
        _defaultRotation = transform.localRotation;
    }

    public override void Attack()
    {
        if (!CanShoot || _isAttacking)
        {
            return;
        }

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        AudioManager.PlaySound(WeaponData.Sound);
        
        _isAttacking = true;

        Quaternion startRotation = transform.localRotation;
        Quaternion leftRotation = Quaternion.Euler(0f, 0f, _leftSwingAngle);
        Quaternion rightRotation = Quaternion.Euler(0f, 0f, _rightSwingAngle);

        float swingDuration = Mathf.Max(0.01f, _swingDuration);
        float phaseDuration = swingDuration / 3f;
        var damagedTargets = new HashSet<IDamageable>();

        float elapsed = 0f;
        while (elapsed < phaseDuration)
        {
            float t = elapsed / phaseDuration;
            transform.localRotation = Quaternion.Slerp(startRotation, leftRotation, t);
            DealDamage(damagedTargets);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = leftRotation;

        elapsed = 0f;
        while (elapsed < phaseDuration)
        {
            float t = elapsed / phaseDuration;
            transform.localRotation = Quaternion.Slerp(leftRotation, rightRotation, t);
            DealDamage(damagedTargets);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = rightRotation;

        elapsed = 0f;
        while (elapsed < phaseDuration)
        {
            float t = elapsed / phaseDuration;
            transform.localRotation = Quaternion.Slerp(rightRotation, _defaultRotation, t);
            DealDamage(damagedTargets);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = _defaultRotation;
        _isAttacking = false;
        ResetCooldown();
    }

    private void DealDamage(HashSet<IDamageable> damagedTargets)
    {
        Vector3 origin = transform.position;
        Vector2 attackDirection = (Vector2)transform.up;
        float halfAttackAngle = _attackAngle * 0.5f;

        Collider2D[] hits2D = Physics2D.OverlapCircleAll(origin, _attackRadius);

        foreach (Collider2D hit in hits2D)
        {
            if (!CanHit(hit))
            {
                continue;
            }

            Vector3 targetPoint = hit.ClosestPoint(origin);

            if (IsTargetOutsideAttackSector(targetPoint, origin, attackDirection, halfAttackAngle))
            {
                continue;
            }

            if (!hit.TryGetComponent(out IDamageable damageable))
            {
                continue;
            }

            if (damagedTargets.Contains(damageable))
            {
                continue;
            }

            damageable.TakeDamage(AttackDamage);
            damagedTargets.Add(damageable);
        }
    }

    private bool IsTargetOutsideAttackSector(Vector3 targetPosition, Vector3 origin, Vector2 attackDirection, float halfAttackAngle)
    {
        Vector2 directionToTarget2D = new Vector2(targetPosition.x - origin.x, targetPosition.y - origin.y);

        if (directionToTarget2D.sqrMagnitude <= 0.0001f)
        {
            return true;
        }

        float angleToTarget = Vector2.Angle(attackDirection.normalized, directionToTarget2D.normalized);

        return angleToTarget > halfAttackAngle;
    }

    private bool CanHit(Collider2D hit)
    {
        if (hit == null || hit.gameObject == gameObject)
        {
            return false;
        }

        if (hit.transform.IsChildOf(transform))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);

        Vector3 origin = transform.position;
        Vector3 direction = transform.up;

        float halfAngle = _attackAngle * 0.5f;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, halfAngle) * direction;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -halfAngle) * direction;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, leftBoundary * _attackRadius);
        Gizmos.DrawRay(origin, rightBoundary * _attackRadius);
    }
}
