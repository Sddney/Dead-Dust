using System;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;

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
            return;
        }

        float fillAmount = Mathf.Clamp01(1f - (currentCooldown / maxCooldown));

        if (fillAmount <= 0f)
        {
            _cooldownImage.fillAmount = 0f;
            return;
        }

        _cooldownImage.fillAmount = fillAmount;
    }

    public void Diactivate()
    {
        _cooldownImage.fillAmount = 1f;
    }
}
