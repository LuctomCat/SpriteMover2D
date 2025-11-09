using UnityEngine;

// Basic asteroid behaviour: drift, rotate, split on destroy (if large)
public class Asteroid : MonoBehaviour
{
    public float minSpeed = 0.5f;
    public float maxSpeed = 2.5f;
    public float rotationMin = -30f;
    public float rotationMax = 30f;
    public GameObject smallerAsteroidPrefab;
    public int size = 2; // 2 = large, 1 = medium, 0 = small

    public float trackingStrength = 0.3f; // How strongly the asteroid turns toward the player
    public float trackingUpdateRate = 0.5f; // How often the asteroid updates its target direction

    Rigidbody2D rb;
    Transform player;
    Vector2 currentVelocity;
    float nextUpdateTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float speed = Random.Range(minSpeed, maxSpeed);
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 vel = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
        rb.linearVelocity = vel;
        currentVelocity = vel;

        rb.angularVelocity = Random.Range(rotationMin, rotationMax);

        // Initial player find
        FindPlayer();
    }

    void FixedUpdate()
    {
        // ✅ Automatically reacquire player if missing (handles respawn)
        if (player == null)
        {
            FindPlayer();
            if (player == null) return; // still no player, skip frame
        }

        // Occasionally adjust velocity toward player
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + trackingUpdateRate;

            Vector2 toPlayer = ((Vector2)player.position - rb.position).normalized;
            Vector2 newDir = Vector2.Lerp(currentVelocity.normalized, toPlayer, trackingStrength).normalized;

            // Keep current speed magnitude
            float speed = currentVelocity.magnitude;
            currentVelocity = newDir * speed;

            rb.linearVelocity = currentVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Asteroid"))
        {
            if (size > 0 && smallerAsteroidPrefab != null)
            {
                SpawnSplit();
            }
            Destroy(gameObject);
            GameManager.Instance?.OnAsteroidDestroyed(transform.position, size);
        }
    }

    void SpawnSplit()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject a = Instantiate(smallerAsteroidPrefab, transform.position, Quaternion.identity);
            Asteroid ast = a.GetComponent<Asteroid>();
            if (ast != null) ast.size = Mathf.Max(0, size - 1);
            Rigidbody2D arb = a.GetComponent<Rigidbody2D>();
            if (arb != null)
            {
                arb.linearVelocity = rb.linearVelocity + Random.insideUnitCircle * 1.5f;
                arb.angularVelocity = Random.Range(-60f, 60f);
            }
        }
    }

    // ✅ Small helper to find the player safely
    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }
}
