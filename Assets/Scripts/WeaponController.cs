using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _allWeapons;

    [SerializeField] private Transform _weaponsTransform;
    [SerializeField] private float _rotationSpeed = 5f;

    private Weapon _currentWeapon;
    UIManager UIManager;

    private bool HasWeapons => _allWeapons.Any();

    private void Start()
    {
        if (HasWeapons)
        {
            _currentWeapon = _allWeapons.First();
            _currentWeapon.gameObject.SetActive(true);
        }
        UIManager = FindObjectOfType<UIManager>();
        if (!UIManager) Debug.LogError("UI Manager is missing.");
    }

    private void Update()
    {
        _weaponsTransform.position = transform.position;

        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction = mouseWorldPosition - _weaponsTransform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        if (_rotationSpeed > 0f)
        {
            _weaponsTransform.rotation = Quaternion.Slerp(_weaponsTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _weaponsTransform.rotation = targetRotation;
        }
    }

    public void Attack()
    {
        if (_currentWeapon is null)
        {
            Debug.LogWarning("No weapon equipped.");
            return;
        }

        _currentWeapon.Attack();
    }

    public void ChangeToNextWeapon()
    {
        if (!HasWeapons)
        {
            Debug.LogWarning("No weapons available to switch.");
            return;
        }

        int currentWeaponIndex = _allWeapons.IndexOf(_currentWeapon);

        int index = currentWeaponIndex + 1;

        if (index >= _allWeapons.Count)
        {
            index = 0;
        }

        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = _allWeapons[index];
        _currentWeapon.gameObject.SetActive(true);

    }

    public void ChangeToPreviousWeapon()
    {
        if (!HasWeapons)
        {
            Debug.LogWarning("No weapons available to switch.");
            return;
        }

        int currentWeaponIndex = _allWeapons.IndexOf(_currentWeapon);
        int index = currentWeaponIndex - 1;

        if (index < 0)
        {
            index = _allWeapons.Count - 1;
        }

        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = _allWeapons[index];
        _currentWeapon.gameObject.SetActive(true);
    }

    public void ChangeWeaponTo(int index)
    {
        if (!HasWeapons)
        {
            Debug.Log("No weapons available to switch.");
            return;
        }

        if (index < 0 || index >= _allWeapons.Count)
        {
            Debug.LogError($"Invalid weapon index: {index}. Available range is 0 to {_allWeapons.Count - 1}.");
            return;
        }

        if (_currentWeapon is not null)
        {
            _currentWeapon.gameObject.SetActive(false);
        }

        _currentWeapon = _allWeapons[index];
        _currentWeapon.gameObject.SetActive(true);
        UIManager.SelectWeapon(index);
    }
}
