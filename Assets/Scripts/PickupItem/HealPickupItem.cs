using UnityEngine;

public class HealPickupItem : PickupItem
{
    [SerializeField] private int _healAmount = 25;

    private Player _player;

    public void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            Pickup();
        }
    }

    public override void Pickup()
    {
        if (_player is null)
        {
            return;
        }

        if (_player.PlayerHealthManagement.IsFullHealth)
        {
            return;
        }

        _player.PlayerHealthManagement.Heal(_healAmount);
        Destroy();
    }
}
