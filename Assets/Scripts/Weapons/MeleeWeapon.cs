using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float _leftSwingAngle = -90f;
    [SerializeField] private float _rightSwingAngle = 90f;
    [SerializeField] private float _attackDistance = 1.25f;
    [SerializeField, Range(0f, 360f)] private float _attackAngle = 180f;
    [SerializeField] private float _attackDuration = 0.2f;
    [SerializeField] private LayerMask _hitLayerMask = -1;

    private bool _isAttacking;

    public override void Attack()
    {
        if (_isAttacking)
        {
            return;
        }

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;

        Quaternion startRotation = transform.localRotation;
        Quaternion leftRotation = Quaternion.Euler(0f, 0f, _leftSwingAngle);
        Quaternion rightRotation = Quaternion.Euler(0f, 0f, _rightSwingAngle);

        float phaseDuration = Mathf.Max(0.01f, _attackDuration / 3f);
        bool hasDealtDamage = false;

        float elapsed = 0f;
        while (elapsed < phaseDuration)
        {
            float t = elapsed / phaseDuration;
            transform.localRotation = Quaternion.Slerp(startRotation, leftRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = leftRotation;

        elapsed = 0f;
        while (elapsed < phaseDuration)
        {
            float t = elapsed / phaseDuration;
            transform.localRotation = Quaternion.Slerp(leftRotation, rightRotation, t);

            if (!hasDealtDamage && elapsed >= phaseDuration * 0.5f)
            {
                DealDamage();
                hasDealtDamage = true;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = rightRotation;

        elapsed = 0f;
        while (elapsed < phaseDuration)
        {
            float t = elapsed / phaseDuration;
            transform.localRotation = Quaternion.Slerp(rightRotation, startRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = startRotation;
        _isAttacking = false;
    }

    private void DealDamage()
    {
        Vector3 origin = transform.position;
        Vector2 attackDirection = transform.parent != null
            ? (Vector2)transform.parent.up
            : (Vector2)transform.up;
        float halfAttackAngle = _attackAngle * 0.5f;

        Collider2D[] hits2D = Physics2D.OverlapCircleAll(origin, _attackDistance, _hitLayerMask);
        Collider2D nearestHit = null;

        IDamageable nearestDamageable = null;
        float nearestDistanceSqr = float.PositiveInfinity;

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

            float distanceSqr = (targetPoint - origin).sqrMagnitude;
            if (distanceSqr < nearestDistanceSqr)
            {
                nearestDistanceSqr = distanceSqr;
                nearestHit = hit;
                nearestDamageable = damageable;
            }
        }

        if (nearestHit != null)
        {
            nearestDamageable.TakeDamage(AttackDamage);
        }
    }

    private bool IsTargetOutsideAttackSector(Vector3 targetPosition, Vector3 origin, Vector2 attackDirection, float halfAttackAngle)
    {
        Vector2 directionToTarget2D = new Vector2(targetPosition.x - origin.x, targetPosition.y - origin.y);

        if (directionToTarget2D.sqrMagnitude <= 0.0001f)
        {
            return true;
        }

        float angleToTarget = Vector2.SignedAngle(attackDirection.normalized, directionToTarget2D.normalized);

        return Mathf.Abs(angleToTarget) > halfAttackAngle;
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
}
