using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private EnemyData data;
    private NavMeshAgent agent;
    private EnemyVision vision;

    private float attackTimer;
    private AudioManager AudioManager;
    private PointsManager PointsManager;

    public void Initialize(EnemyData enemyData)
    {
        data = enemyData;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AudioManager = FindAnyObjectByType<AudioManager>();
        vision = GetComponent<EnemyVision>();

        if (AudioManager is null)
            Debug.LogError("AudioManager not found in the scene. Please make sure there is an AudioManager in the scene.");

        PointsManager = FindAnyObjectByType<PointsManager>();
        if (PointsManager is null)
            Debug.LogError("PointsManager not found in the scene. Please make sure there is a PointsManager in the scene.");
    }

    private void FixedUpdate()
    {
        if (attackTimer > 0)
            attackTimer -= Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (data.enemyType == EnemyType.Ranged)
        {
            HandleRangedAttack();
        }
    }

    private void HandleRangedAttack()
    {
        if (vision.Player is null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, vision.Player.position);

        bool canAttack = distance <= data.attackRange && vision.CanSeePlayer;

        agent.isStopped = canAttack;

        if (canAttack && attackTimer <= 0)
        {
            Shoot();
            attackTimer = data.attackCooldown;
        }
    }

    private void Shoot()
    {
        AudioManager.PlaySound(data.soundShot);
        GameObject projectile = Instantiate(data.projectilePrefab, transform.position, Quaternion.identity);

        Vector2 direction = (vision.Player.position - transform.position).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.linearVelocity = direction * data.projectileSpeed;

        Destroy(projectile, 3f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (data.enemyType == EnemyType.Ranged)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (attackTimer > 0)
            return;

        PlayerHealthManagement playerHealth =
            other.GetComponent<PlayerHealthManagement>();

        if (playerHealth == null)
            return;

        playerHealth.Damage((int)data.damage);

        attackTimer = data.attackCooldown;
    }
}
