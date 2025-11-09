using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    void Start()
    {
        // Initialize health at start
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (!IsAlive())
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void Die()
    {
        Death death = GetComponent<Death>();
        if (death != null)
        {
            death.Die();
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
