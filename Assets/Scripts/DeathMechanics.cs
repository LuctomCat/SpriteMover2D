using UnityEngine;

public class DeathMechanics : Death
{
    public override void Die()
    {
        // Notify GameManager that the player was destroyed
        GameManager.Instance.OnPlayerDestroyed();

        // Destroy the player object
        Destroy(gameObject);
    }
}
