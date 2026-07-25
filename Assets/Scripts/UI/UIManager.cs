using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ShieldUI _shieldUI;
    [SerializeField] private DeathUI _deathUI;
    [SerializeField] private RectTransform _selection;
    [SerializeField] private RectTransform[] _weaponIcons;


    [SerializeField] private RectTransform selection;
    [SerializeField] private RectTransform[] weaponIcons;
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private TextMeshProUGUI pointsText;


    [SerializeField] Image healthBar;
    [SerializeField] private Image _healthBar;

    public void SelectWeapon(int weaponIndex)
    {
        _selection.anchoredPosition = _weaponIcons[weaponIndex].anchoredPosition;

    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_healthBar is null)
        {
            return;
        }

        _healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        pointsText.text = "dust bunny: " + pointsManager.killedMelee.ToString() + "\nsticky spot: " + pointsManager.killedTank.ToString() + "\nsplitter: " + pointsManager.killedRanged.ToString();
    }

    public void UpdateShield(float currentCooldown, float maxCooldown)
    {
        _shieldUI.UpdateImage(currentCooldown, maxCooldown);
    }

    public void ShowDeathUI()
    {
        _deathUI.gameObject.SetActive(true);
    }
}
