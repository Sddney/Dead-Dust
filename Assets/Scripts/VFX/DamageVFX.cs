using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageVFX : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 3;
    [SerializeField] private float alpha = 0.6f;

    private Coroutine flashRoutine;


    public void PlayPlayerDamage(SpriteRenderer spriteRenderer)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(Flash(spriteRenderer));
    }

    private IEnumerator Flash(SpriteRenderer spriteRenderer)
    {
        Color color = spriteRenderer.color;

        for (int i = 0; i < flashCount; i++)
        {
            color.a = alpha;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(flashDuration);

            color.a = 1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(flashDuration);
        }
    }
}