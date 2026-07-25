using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform selection;
    [SerializeField] private RectTransform[] weaponIcons;

    public void SelectWeapon(int weaponIndex)
    {
        selection.anchoredPosition = weaponIcons[weaponIndex].anchoredPosition;
        
    }

}
