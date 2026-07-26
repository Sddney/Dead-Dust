using UnityEngine;
using TMPro;

public class FightTextManager : MonoBehaviour
{
    public static FightTextManager Instance { get; private set; }

    [SerializeField] private FloatingText floatingTextPrefab;

    [Header("Hit")]
    [Range(0, 1)]   
    [SerializeField] private float hitChance = 0.3f;

    [SerializeField] private string[] hitTexts =
    {
        "Clean Hit!",
        "Sparkle!",
        "Fresh!",
        "Spotless!"
    };

    [Header("Kill")]
    [SerializeField] private string[] killTexts =
    {
        "Cleaned Up!",
        "Trash Taken Out!",
        "Deep Clean!",
        "Sanitized!",
        "Job Done!"
    };


    [SerializeField] private Canvas worldCanvas;

    [SerializeField] private Color[] textColors;


    private void Awake()
    {
        Instance = this;
    }

    public void ShowHit(Vector3 worldPosition)
    {
        if (Random.value > hitChance)
            return;

        Spawn(worldPosition, hitTexts[Random.Range(0, hitTexts.Length)]);
    }

    public void ShowKill(Vector3 worldPosition)
    {
        Spawn(worldPosition, killTexts[Random.Range(0, killTexts.Length)]);
    }

    private void Spawn(Vector3 worldPosition, string text)
    {
        FloatingText popup = Instantiate(floatingTextPrefab, worldCanvas.transform);

        popup.transform.position = worldPosition;

        Color randomColor = textColors[Random.Range(0, textColors.Length)];

        popup.SetText(text, randomColor);
    }
}