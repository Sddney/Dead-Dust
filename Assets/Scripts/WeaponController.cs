using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _allWeapons;
    [SerializeField] private SpecialWeapon _specialWeapon;
    [SerializeField] private float _rotationSpeed = 5f;

    private Weapon _currentWeapon;
    private UIManager _uiManager;

    private Animator _animator;

    private float _specialWeaponCooldown;
    private bool _specialWeaponInUse = false;
    private bool _canUseSpecialWeapon = true;

    private bool HasWeapons => _allWeapons.Any();

    public event EventHandler SpecialWeaponActivated;
    public event EventHandler SpecialWeaponDiactivated;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (HasWeapons)
        {
            _currentWeapon = _allWeapons.First();
            _currentWeapon.gameObject.SetActive(true);
        }

        _uiManager = FindAnyObjectByType<UIManager>();
        if (!_uiManager)
        {
            Debug.LogWarning("UI Manager is missing.");
        }
    }

    private void Update()
    {
        if (_canUseSpecialWeapon)
        {
            return;
        }

        _specialWeaponCooldown += Time.deltaTime;

        if (_uiManager is not null)
        {
            _uiManager.UpdateShield(_specialWeaponCooldown, _specialWeapon.Cooldown);
        }

        if (_specialWeaponCooldown >= _specialWeapon.Cooldown)
        {
            _canUseSpecialWeapon = true;
        }
    }

    public void Attack()
    {
        if (_specialWeaponInUse)
        {
            return;
        }

        if (_currentWeapon is null)
        {
            Debug.LogWarning("No weapon equipped.");
            return;
        }

        if (_currentWeapon.CanShoot)
        {
            if (_currentWeapon is MeleeWeapon melee)
            {
                _animator.SetTrigger("Attack1");
            }

            if (_currentWeapon is RangedWeapon ranged)
            {
                _animator.SetTrigger("Attack2");
            }
        }

        _currentWeapon.Attack();
    }

    public void ActivateSpecialWeapon()
    {
        if (_specialWeaponInUse)
        {
            Debug.LogWarning("Special weapon is on cooldown.");
            return;
        }

        if (!_canUseSpecialWeapon)
        {
            return;
        }

        StartCoroutine(UseSpecialWeapon());
    }

    private IEnumerator UseSpecialWeapon()
    {
        _specialWeaponInUse = true;
        SpecialWeaponActivated?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(_specialWeapon.ShieldDuration);

        SpecialWeaponDiactivated?.Invoke(this, EventArgs.Empty);
        _specialWeapon.Attack();

        _specialWeaponInUse = false;
        _canUseSpecialWeapon = false;
        _specialWeaponCooldown = 0;
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

        if (_uiManager is not null)
        {
            _uiManager.SelectWeapon(index);
        }
    }
}
