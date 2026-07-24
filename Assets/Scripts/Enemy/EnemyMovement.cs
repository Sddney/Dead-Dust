using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private Transform playerTransform;
    private NavMeshAgent navAgent;

    public void Initialize(EnemyData data)
    {
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.speed = data.moveSpeed;
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    void Update()
    {

        navAgent.SetDestination(playerTransform.position);
        RotateTowardsVelocity();

    }

      private void RotateTowardsVelocity()
    {
        
        if (navAgent.velocity.sqrMagnitude < 0.01f)
            return;

        float angle = Mathf.Atan2(
            navAgent.velocity.y,
            navAgent.velocity.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

}
