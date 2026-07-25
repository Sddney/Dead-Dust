using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private float _speed = 10;
    private float _distance = 5;

    private float _damage = 0;

    private Vector3 _initialPosition;
    private Vector2 _currentVelocity;
    private float _smoothTime = 0.05f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _initialPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector3.Distance(_initialPosition, transform.position) <= _distance)
        {
            return;
        }

        DestroyProjectile();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = transform.up * _speed;
        _rigidbody.linearVelocity = Vector2.SmoothDamp(
            _rigidbody.linearVelocity,
            targetVelocity,
            ref _currentVelocity,
            _smoothTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetAttackDamage(float attackDamage)
    {
        if (attackDamage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(attackDamage), "Attack damage cannot be negative.");
        }

        _damage = attackDamage;
    }

    public void SetSpeedAndDistance(float speed, float distance)
    {
        if (speed < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(speed), "Speed cannot be negative.");
        }

        if (distance < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distance), "Distance cannot be negative.");
        }

        _distance = distance;
        _speed = speed;
    }
}
