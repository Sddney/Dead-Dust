using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private LayerMask visionMask;

    private Transform player;

    public bool CanSeePlayer { get; private set; }

    public Transform Player => player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player is null)
        {
            return;
        }

        CanSeePlayer = CheckVision();
    }

    private bool CheckVision()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            distance,
            visionMask);

        Debug.DrawRay(transform.position, direction * distance, Color.red);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }
}
