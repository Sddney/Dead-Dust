using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private ShieldUI _shieldUI;
    [SerializeField] private DeathUI _deathUI;
    [SerializeField] private GameObject[] _weaponsSelections;

    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private TextMeshProUGUI pointsText;

    [SerializeField] private Image _healthBar;

    public void SelectWeapon(int weaponIndex)
    {
        foreach (var item in _weaponsSelections)
        {
            item.SetActive(false);
        }

        _weaponsSelections[weaponIndex].SetActive(true);
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
        _mainUI.SetActive(false);
        _deathUI.gameObject.SetActive(true);
    }
}
