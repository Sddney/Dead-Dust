using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{


    [SerializeField] private RectTransform selection;
    [SerializeField] private RectTransform[] weaponIcons;
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private TextMeshProUGUI pointsText;


    [SerializeField] Image healthBar;

    public void SelectWeapon(int weaponIndex)
    {
        selection.anchoredPosition = weaponIcons[weaponIndex].anchoredPosition;
        
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        pointsText.text = "dust bunny: "+pointsManager.killedMelee.ToString()+"\nsticky spot: "+pointsManager.killedTank.ToString()+"\nsplitter: "+pointsManager.killedRanged.ToString();
    }

}
