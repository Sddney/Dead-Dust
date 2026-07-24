using UnityEngine;

public class ShootDamage : MonoBehaviour
{

    [SerializeField] private int damage;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthManagement playerHealth = other.GetComponent<PlayerHealthManagement>();
            playerHealth.Damage(8);
        }
    }
    
}
