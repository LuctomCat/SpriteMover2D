using UnityEngine;
using UnityEngine.UI;

// Handles the on-screen UI for score, lives, and player health
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    public Text scoreText;
    public Text livesText;
    public Text healthText;

    GameManager gameManager;
    Health playerHealth;

    Canvas uiCanvas;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        CreateUI();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        playerHealth = FindFirstObjectByType<Health>();
    }

    void Update()
    {
        if (gameManager != null)
        {
            scoreText.text = "Score: " + GetPrivateField<int>(gameManager, "score").ToString();
            livesText.text = "Lives: " + GetPrivateField<int>(gameManager, "lives").ToString();
        }

        if (playerHealth != null)
        {
            float percent = Mathf.Clamp01(playerHealth.currentHealth / Mathf.Max(1f, playerHealth.maxHealth));
            healthText.text = "Health: " + Mathf.RoundToInt(percent * 100f) + "%";
        }
    }

    void CreateUI()
    {
        // Create Canvas
        GameObject canvasGO = new GameObject("GameUI");
        uiCanvas = canvasGO.AddComponent<Canvas>();
        uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Score Text (Top Left)
        scoreText = CreateUIText("ScoreText", new Vector2(0, 1), new Vector2(0, 1), new Vector2(10, -10));
        scoreText.alignment = TextAnchor.UpperLeft;
        scoreText.text = "Score: 0";

        // Lives Text (Top Right)
        livesText = CreateUIText("LivesText", new Vector2(1, 1), new Vector2(1, 1), new Vector2(-10, -10));
        livesText.alignment = TextAnchor.UpperRight;
        livesText.text = "Lives: 3";

        // Health Text (Bottom Center)
        healthText = CreateUIText("HealthText", new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0, 10));
        healthText.alignment = TextAnchor.LowerCenter;
        healthText.text = "Health: 100%";
    }

    Text CreateUIText(string name, Vector2 anchorMin, Vector2 anchorMax, Vector2 offset)
    {
        GameObject textGO = new GameObject(name);
        textGO.transform.SetParent(uiCanvas.transform);

        RectTransform rect = textGO.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = anchorMin;
        rect.anchoredPosition = offset;

        Text text = textGO.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = 24;
        text.color = Color.white;

        return text;
    }

    // Reflection helper to access private fields in GameManager without modifying it
    T GetPrivateField<T>(object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (T)field.GetValue(obj);
    }
}
