using Unity.VisualScripting;
using UnityEngine;

// Controls a top-down ship (rotation + thrust) using local-space controls (W/S forward/back, A/D rotate)
// Works with a Rigidbody2D. Attach this to the Pawn (player ship) GameObject.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{

    // Life and Death Components -------------------------- HEALTH AND DEATH
    [Header("Components")]
    public Health health;
    public Death death;

    void Start()
    {
        //Load health component
        health = GetComponent<Health>();
        //Load Death Component
        death = GetComponent<Death>();        
    }


    // MOVEMENT --------
    public float thrustForce = 5f;
    public float rotationSpeed = 200f; // degrees per second
    public float maxSpeed = 6f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float shootCooldown = 0.25f;

    Rigidbody2D rb;
    float shootTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Rotation (A/D or Left/Right)
        float rot = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) rot = 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) rot = -1f;
        transform.Rotate(0f, 0f, rot * rotationSpeed * Time.deltaTime);

        // Thrust (W/S or Up/Down)
        float thrust = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) thrust = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) thrust = -0.5f;

        Vector2 force = transform.up * (thrust * thrustForce);
        rb.AddForce(force);

        // Cap speed
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // Shooting
        shootTimer -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || bulletSpawn == null) return;
        GameObject b = Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        Rigidbody2D br = b.GetComponent<Rigidbody2D>();
        if (br) br.linearVelocity = new Vector2(b.transform.up.x, b.transform.up.y) * 12f + rb.linearVelocity; // bullet inherits ship velocity
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        health.TakeDamage(1.0f);
    }

}
