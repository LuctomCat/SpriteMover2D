using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab;
    public Transform asteroidsParent;
    public int startingAsteroids = 4;
    public List<Transform> asteroidSpawnPoints;

    [Header("Player Settings")]
    public GameObject playerPrefab;
    public Transform playerParent;
    public Vector3 playerSpawnPosition = Vector3.zero;
    public float respawnDelay = 2f;
    public int startingLives = 3;

    [Header("UI")]
    public Canvas gameOverCanvas;
    public Text livesText;

    private int score = 0;
    private int lives;
    private GameObject currentPlayer;
    private bool isGameOver = false;
    private bool isRespawning = false;

    public int Score => score;
    public int Lives => lives;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        lives = startingLives;
        UpdateLivesUI();
        SpawnInitialAsteroids();
        SpawnPlayer();

        // Hide Canvas at Start
        if (gameOverCanvas != null)
            gameOverCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    void SpawnInitialAsteroids()
    {
        for (int i = 0; i < startingAsteroids; i++)
        {
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        Vector2 pos = Random.insideUnitCircle.normalized * 10f;
        GameObject a = Instantiate(asteroidPrefab, new Vector3(pos.x, pos.y, 0f),
            Quaternion.Euler(0, 0, Random.Range(0f, 360f)), asteroidsParent);
    }

    public void OnAsteroidDestroyed(Vector3 position, int size)
    {
        int gained = (size + 1) * 100;
        score += gained;

        if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0)
        {
            startingAsteroids++;
            SpawnInitialAsteroids();
        }
    }

    public void OnPlayerDestroyed()
    {
        if (isGameOver || isRespawning) return;
        isRespawning = true;

        lives--;
        UpdateLivesUI();

        if (lives > 0)
        {
            StartCoroutine(RespawnPlayerAfterDelay());
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator RespawnPlayerAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (!isGameOver)
        {
            SpawnPlayer();
        }

        isRespawning = false;
    }

    void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab not assigned in GameManager!");
            return;
        }

        currentPlayer = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity, playerParent);

        // Reset player health on respawn
        Health health = currentPlayer.GetComponent<Health>();
        if (health != null)
        {
            health.currentHealth = health.maxHealth;
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    void GameOver()
    {
        isGameOver = true;
        isRespawning = false;

        // Activate "Game Over" Canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverCanvas not assigned in GameManager!");
        }

        // Destroy remaining player instances
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
    }
}
