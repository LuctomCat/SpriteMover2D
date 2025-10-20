using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public KeyCode keyToUse;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToUse))
        {
            // Change Sprite Color
            Color newColor = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
            spriteRenderer.color = newColor;
        }
    }
}
