using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _smoothTime = 0.05f;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private Vector2 _currentVelocity;
    
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;

    UIManager ui;

    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        ui = FindAnyObjectByType<UIManager>();
        if(ui is null) Debug.LogError("UI Manager not found!");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        RotateTowardsMouse();

        if(isDashing) return;

        Vector2 targetVelocity = _moveInput * _speed;

        _rigidbody.linearVelocity = Vector2.SmoothDamp(
            _rigidbody.linearVelocity,
            targetVelocity,
            ref _currentVelocity,
            _smoothTime);

    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && !isDashing)
        {
            StartCoroutine(Dash());
            ui.DashAnimation();
        }
    }

    private void RotateTowardsMouse()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        if (direction == Vector2.zero) return;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        if (_rotationSpeed > 0f)
        {
            float currentAngle = _rigidbody.rotation;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, _rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(newAngle);
        }
        else
        {
            _rigidbody.MoveRotation(targetAngle);
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDirection = _moveInput.normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = transform.up;
        }

        float timer = 0f;

        while (timer < dashDuration)
        {
            _rigidbody.linearVelocity = dashDirection * dashSpeed;

            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}