using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ShieldUI _shieldUI;
    [SerializeField] private RectTransform _selection;
    [SerializeField] private RectTransform[] _weaponIcons;

    [SerializeField] private Image _healthBar;

    public void SelectWeapon(int weaponIndex)
    {
        _selection.anchoredPosition = _weaponIcons[weaponIndex].anchoredPosition;
        
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateShield(float currentCooldown, float maxCooldown)
    {
        _shieldUI.UpdateImage(currentCooldown, maxCooldown);
    }
}
