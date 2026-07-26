using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private ShieldUI _shieldUI;
    [SerializeField] private DeathUI _deathUI;
    [SerializeField] private WinUI _winUI;
    [SerializeField] private GameObject[] _weaponsSelections;

    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private TextMeshProUGUI pointsText;
    EnemySpawner enemySpawner;

    [SerializeField] private Image _healthBar;

    [SerializeField] AudioClip GameOverSound;
    [SerializeField] AudioClip VictorySound;
    AudioManager audioManager;


    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }
    

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
        pointsText.text = "dust bunny: " + enemySpawner.normalCount +"/"+ pointsManager.killedMelee.ToString() 
        + "\nsticky spot: " + enemySpawner.tankCount +"/"+ pointsManager.killedTank.ToString() + 
        "\nsplitter: " + enemySpawner.rangedCount +"/"+ pointsManager.killedRanged.ToString();
    }

    public void UpdateShield(float currentCooldown, float maxCooldown)
    {
        _shieldUI.UpdateImage(currentCooldown, maxCooldown);
    }

    public void DiactivateShield()
    {
        _shieldUI.Diactivate();
    }

    public void ShowDeathUI()
    {
        _mainUI.SetActive(false);
        _deathUI.gameObject.SetActive(true);
        audioManager.PlaySound(GameOverSound);
        audioManager.LowerVolume(0.2f);

    }

    public void ShowVictoryUI()
    {
         _mainUI.SetActive(false);
         _winUI.gameObject.SetActive(true);
         audioManager.LowerVolume(0.2f);
         audioManager.PlaySound(VictorySound);

    }
}
