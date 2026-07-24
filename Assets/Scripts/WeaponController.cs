using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _allWeapons;

    private Weapon _currentWeapon;

    private bool HasWeapons => _allWeapons.Any();

    private void Start()
    {
        if (HasWeapons)
        {
            _currentWeapon = _allWeapons.First();
            _currentWeapon.gameObject.SetActive(true);
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

        Debug.Log(index);

        _currentWeapon = _allWeapons[index];
        _currentWeapon.gameObject.SetActive(true);
    }
}
