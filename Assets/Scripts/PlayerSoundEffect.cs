using UnityEngine;

// Handles sound effects for the player ship (shooting and taking damage)
public class PlayerSoundEffects : MonoBehaviour
{
    private PlayerControl player;
    private Health health;

    private float lastHealth;

    void Start()
    {
        player = GetComponent<PlayerControl>();
        health = GetComponent<Health>();

        if (health != null)
        {
            lastHealth = health.currentHealth;
        }
    }

    void Update()
    {
        // Detect shooting without modifying PlayerControl
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayBlaster();
            }
        }

        // Detect damage by checking if health decreases
        if (health != null && health.currentHealth < lastHealth)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayExplosion();
            }
            lastHealth = health.currentHealth;
        }

        // Update health tracker
        if (health != null)
        {
            lastHealth = health.currentHealth;
        }
    }
}
