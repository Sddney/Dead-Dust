using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private float _speed = 10;
    private float _distance = 5;

    private int _damage = 0;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = transform.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void SetAttackDamage(int attackDamage)
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
