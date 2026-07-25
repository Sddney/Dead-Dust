using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [SerializeField] private RectTransform selection;
    [SerializeField] private RectTransform[] weaponIcons;


    [SerializeField] Image healthBar;

    public void SelectWeapon(int weaponIndex)
    {
        selection.anchoredPosition = weaponIcons[weaponIndex].anchoredPosition;
        
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

}
