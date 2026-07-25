using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int killedMelee;
    public int killedTank;
    public int killedRanged;

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
    }
}
