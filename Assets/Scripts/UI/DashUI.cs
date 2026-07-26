using System.Collections;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    [SerializeField] private RectTransform dashIcon;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float duration = 0.1f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = dashIcon.localScale;
    }

    public void StartAnim()
    {
        StartCoroutine(DashIconAnimation());
    }

    private IEnumerator DashIconAnimation()
    {
        Vector3 targetScale = originalScale * scaleMultiplier;

        float t = 0f;


        while (t < duration)
        {
            t += Time.deltaTime;
            dashIcon.localScale = Vector3.Lerp(originalScale, targetScale, t / duration);
            yield return null;
        }

        t = 0f;
        
        while (t < duration)
        {
            t += Time.deltaTime;
            dashIcon.localScale = Vector3.Lerp(targetScale, originalScale, t / duration);
            yield return null;
        }

        dashIcon.localScale = originalScale;
    }
}