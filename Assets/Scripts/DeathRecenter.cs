using UnityEngine;

public class DeathRecenter : Death
{
    public override void Die()
    {
        transform.position = Vector3.zero;

        // Notify GameManager that player "died" and will respawn
        GameManager.Instance?.OnPlayerDestroyed();

        base.Die();
    }
}
