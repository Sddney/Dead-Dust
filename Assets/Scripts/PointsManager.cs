using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int killedMelee;
    public int killedTank;
    public int killedRanged;

    [SerializeField] private EnemySpawner spawner;

    UIManager ui;

    void Start()
    {
        spawner = FindAnyObjectByType<EnemySpawner>();
        if (spawner == null) Debug.LogError("No spawner found");
        ui = FindAnyObjectByType<UIManager>();
    }

    public void AddKilled(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Melee:
                killedMelee++;
                break;
            case EnemyType.Tank:
                killedTank++;
                break;
            case EnemyType.Ranged:
                killedRanged++;
                break;
        }

        int totalKilled = killedMelee + killedTank + killedRanged;

        if (totalKilled >= spawner.TotalEnemies)
        {
            ui.ShowVictoryUI();
            Time.timeScale = 0f;
        }
    }


}
