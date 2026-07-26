using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    public EnemyData Data => enemyData;

    private EnemyHealth health;
    private EnemyMovement movement;
    private EnemyAttack attack;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();

        health.Initialize(enemyData);
        movement.Initialize(enemyData);
        attack.Initialize(enemyData);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyData.sprite;
        spriteRenderer.color = enemyData.color;
        transform.localScale = Vector3.one * enemyData.scale;

    }
}
