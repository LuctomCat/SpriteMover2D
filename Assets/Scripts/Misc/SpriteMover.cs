using UnityEngine;

public class SpriteMover : MonoBehaviour
{
    public float xRange = 5f;
    public float yRange = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Position Jumps and Input Key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetRandomPosition();
        }
    }

    void SetRandomPosition()
    {
        float randomX = Random.Range(-xRange, xRange);
        float randomY = Random.Range(-yRange, yRange);
        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }
}
