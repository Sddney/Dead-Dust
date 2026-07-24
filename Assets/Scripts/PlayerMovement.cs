using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothTime = 0.05f; 

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * speed;
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity,targetVelocity,ref currentVelocity,smoothTime);
    }
}