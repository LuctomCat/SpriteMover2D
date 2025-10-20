using UnityEngine;
using System.Collections.Generic;

// Minimal singleton GameManager to spawn asteroids and track score / waves
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject asteroidPrefab;
    public Transform asteroidsParent;
    public int startingAsteroids = 4;
    public List<Transform> asteroidSpawnPoints;

    int score = 0;
    int lives = 3;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        SpawnInitialAsteroids();
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
}
