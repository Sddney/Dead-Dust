using UnityEngine;


public class VFXManager : MonoBehaviour
{

    [SerializeField] DamageVFX damageVFX;
    [SerializeField] GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerDamagePlay()
    {
        damageVFX.PlayPlayerDamage(player.GetComponentInChildren<SpriteRenderer>());
        
    }

    public void EnemyDamagePlay(Transform enemyTransform)
    {
        Debug.Log("ENEMY HIT");
        damageVFX.EnemyDamageVFX(enemyTransform);
    }
    
}