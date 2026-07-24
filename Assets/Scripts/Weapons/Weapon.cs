using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _cooldown;

    private bool _canShoot = true;
    private float _currentCooldown;

    public bool CanShoot => _canShoot;

    public int AttackDamage => _attackDamage;

    private void Update()
    {
        _currentCooldown += Time.deltaTime;
        if (_currentCooldown >= _cooldown)
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
