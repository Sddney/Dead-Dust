using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [field: SerializeField] public float AttackDamage { get; private set; }

    [field: SerializeField] public float AttackDistance { get; private set; }

    [field: SerializeField] public float Cooldown { get; private set; }

    [field: SerializeField] public AudioClip Sound { get; private set; }
}
