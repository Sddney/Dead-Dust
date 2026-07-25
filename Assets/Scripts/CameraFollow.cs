using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    void FixedUpdate()
    {
        transform.position = player.position + offset * Time.deltaTime;
    }
}