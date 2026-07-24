using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _attackDamage;

    public int AttackDamage => _attackDamage;

    public abstract void Attack();
}
