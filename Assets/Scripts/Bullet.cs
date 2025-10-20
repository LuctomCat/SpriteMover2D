using UnityEngine;

// Simple bullet that moves and destroys itself after a lifetime or when colliding with an asteroid
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Expect Asteroid to have tag "Asteroid"
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
