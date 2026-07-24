using UnityEngine;

public class Test : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log($"{name}:{damage}");
    }
}
