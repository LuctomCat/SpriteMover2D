using UnityEngine;

// Reports asteroid destruction to the GameManager for score tracking
public class AsteroidDeathReporter : MonoBehaviour
{
    public int size = 0;
    public int scoreValue = 100;

    void Start()
    {
        
    }

    // This method should be called when the asteroid dies
    public void ReportDeath()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnAsteroidDestroyed(transform.position, size);
        }
    }

    // If this asteroid has a Death component that destroys it,
    void OnDestroy()
    {
        if (Application.isPlaying)
        {
            ReportDeath();
        }
    }
}
