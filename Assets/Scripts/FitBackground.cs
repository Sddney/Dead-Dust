using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitBackground : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldHeight = cam.orthographicSize * 2f + 0.5f;
        float worldWidth = worldHeight * cam.aspect  + 0.5f;

        Vector2 spriteSize = sr.sprite.bounds.size;

        transform.localScale = new Vector3(worldWidth / spriteSize.x, worldHeight / spriteSize.y, 1f);
    }
}