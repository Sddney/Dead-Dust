using System;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    public event EventHandler PickupItemDestroyed;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            Pickup();
            Destroy();
        }
    }

    protected void OnPickupItemDestroyed()
    {
        PickupItemDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public abstract void Pickup();

    public virtual void Destroy()
    {
        Destroy(gameObject);
        PickupItemDestroyed?.Invoke(this, EventArgs.Empty);
    }
}
