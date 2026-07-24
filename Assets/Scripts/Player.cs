using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || _currentWeapon == null)
        {
            return;
        }

        _currentWeapon.Attack();
    }
}
