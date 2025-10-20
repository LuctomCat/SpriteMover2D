using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Health : MonoBehaviour

{
    public float currentHealth;
    public float maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // How Much Damage Is Taken
    public void TakeDamage(float amount)
    {
        currentHealth = currentHealth - amount;
        if (!IsAlive())
        {
            currentHealth = 0;
            Die();
        }
    }

    // How Much Healing Is Done
    public void Heal(float amount)
    {
        currentHealth = currentHealth + amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Player Death Mechanic
    public void Die()
    {
        //Create Death Component
        Death death = GetComponent<Death>();
        // If that death component exists
        if (death != null)
        {
            death.Die();
        }
    }


    // Check If Player Is Alive
    public bool IsAlive()
    {
        if (currentHealth > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
