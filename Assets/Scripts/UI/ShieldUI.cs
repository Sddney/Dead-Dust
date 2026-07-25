using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;

    private bool _isAvailable = true;

    public void UpdateImage(float currentCooldown, float maxCooldown)
    {
        if (_cooldownImage is null)
        {
            Debug.LogError("Cooldown image is not assigned in ShieldUI.");
            return;
        }

        if (maxCooldown <= 0f || currentCooldown <= 0f)
        {
            _cooldownImage.fillAmount = 0f;
            _isAvailable = true;
            return;
        }

        float fillAmount = Mathf.Clamp01(1f - (currentCooldown / maxCooldown));

        if (fillAmount <= 0f)
        {
            _cooldownImage.fillAmount = 0f;
            _isAvailable = true;
            return;
        }

        _cooldownImage.fillAmount = fillAmount;
        _isAvailable = false;
    }
}
