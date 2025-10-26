using UnityEngine;

public class ScoreDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && GameManager.Instance != null)
        {
            // Manually simulate destroying a size-1 asteroid
            GameManager.Instance.OnAsteroidDestroyed(Vector3.zero, 1);
            Debug.Log("Manual score increase triggered.");
        }
    }
}
