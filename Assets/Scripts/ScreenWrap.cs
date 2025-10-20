using UnityEngine;

// Simple screen wrap for top-down 2D playfield. Attach to objects that should wrap (ship, asteroids, bullets)
public class ScreenWrap : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 viewport = cam.WorldToViewportPoint(pos);
        bool wrapped = false;

        if (viewport.x < 0f) { viewport.x = 1f; wrapped = true; }
        else if (viewport.x > 1f) { viewport.x = 0f; wrapped = true; }
        if (viewport.y < 0f) { viewport.y = 1f; wrapped = true; }
        else if (viewport.y > 1f) { viewport.y = 0f; wrapped = true; }

        if (wrapped)
        {
            Vector3 newPos = cam.ViewportToWorldPoint(viewport);
            newPos.z = pos.z;
            transform.position = newPos;
        }
    }
}
