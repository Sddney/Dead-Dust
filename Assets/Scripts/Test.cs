using UnityEngine;

public class Test : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"{name}:{damage}");
    }
}
