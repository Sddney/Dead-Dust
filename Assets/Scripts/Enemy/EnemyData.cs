using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("General")]
    public string enemyName;
    public EnemyType enemyType;

    [Header("Stats")]
    public float maxHealth = 100;
    public float moveSpeed = 3;
    public float damage = 10;

    [Header("Appearance")]
    public Sprite sprite;
    public Color color = Color.white;
    public float scale = 1f;

    [Header("Attack")]
    public float attackRange = 1f;
    public float attackCooldown = 1f;

    [Header("Ranged Only")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;
    public AudioClip soundShot;

    [Header("Audio")] 
    public AudioClip hurtSound;
}