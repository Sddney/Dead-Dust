using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _shield;

    private PlayerHealthManagement _healthManagement;
    private WeaponController _weaponManager;

    public PlayerHealthManagement PlayerHealthManagement => _healthManagement;

    private void Awake()
    {
        _weaponManager = GetComponent<WeaponController>();
        _healthManagement = GetComponent<PlayerHealthManagement>();
        _weaponManager.SpecialWeaponActivated += HandleSpecialWeaponActivated;
        _weaponManager.SpecialWeaponDiactivated += SpecialWeaponDiactivated;
    }

    private void OnDestroy()
    {
        _weaponManager.SpecialWeaponActivated -= HandleSpecialWeaponActivated;
        _weaponManager.SpecialWeaponDiactivated -= SpecialWeaponDiactivated;
    }

    private void HandleSpecialWeaponActivated(object sender, System.EventArgs e)
    {
        _shield.gameObject.SetActive(true);
        _healthManagement.ActivateShield(true);
    }

    private void SpecialWeaponDiactivated(object sender, System.EventArgs e)
    {
        _shield.gameObject.SetActive(false);
        _healthManagement.ActivateShield(false);
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
