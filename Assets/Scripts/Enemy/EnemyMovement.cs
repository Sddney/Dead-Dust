using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    private bool canMove = true;

    public void Initialize(EnemyData data)
    {
        navMeshAgent ??= GetComponent<NavMeshAgent>();
        navMeshAgent.speed = data.moveSpeed;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        navMeshAgent ??= GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (playerTransform is null || !canMove)
        {
            return;
        }

        navMeshAgent.SetDestination(playerTransform.position);
        RotateTowardsVelocity();
    }

    private void RotateTowardsVelocity()
    {
        if (navMeshAgent.velocity.sqrMagnitude < 0.01f)
        {
            return;
        }

        float angle = Mathf.Atan2(
            navMeshAgent.velocity.y,
            navMeshAgent.velocity.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void DisableMovement()
    {
        canMove = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
    }
}
