using UnityEngine;

public class HealPickupItem : PickupItem
{
    [SerializeField] private int _healAmount = 25;

    private Player _player;

    public void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    public override void Pickup()
    {
        if (_player is null)
        {
            return;
        }

        _player.PlayerHealthManagement.Heal(_healAmount);
    }
}
