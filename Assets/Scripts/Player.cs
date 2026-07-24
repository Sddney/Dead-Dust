using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponController))]
public class Player : MonoBehaviour
{
    private WeaponController _weaponManager;

    private void Awake()
    {
        _weaponManager = GetComponent<WeaponController>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || _weaponManager == null)
        {
            return;
        }

        _weaponManager.Attack();
    }
}
