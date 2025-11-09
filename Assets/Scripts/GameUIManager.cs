using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    public Text scoreText;
    public Text livesText;
    public Text healthText;

    private GameManager gameManager;
    private Health playerHealth;
    private Canvas uiCanvas;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        CreateUI();
    }

    IEnumerator Start()
    {
        // Wait until GameManager and player exist
        yield return new WaitUntil(() => GameManager.Instance != null);
        gameManager = GameManager.Instance;

        yield return new WaitUntil(() => FindFirstObjectByType<Health>() != null);
        playerHealth = FindFirstObjectByType<Health>();
    }

    void Update()
    {
        // UPD Player Health Value
        if (playerHealth == null)
        {
            playerHealth = FindFirstObjectByType<Health>();
        }

        if (gameManager != null)
        {
            scoreText.text = "SCORE: " + gameManager.Score;
            livesText.text = "LIVES: " + gameManager.Lives;
        }

        if (playerHealth != null)
        {
            float percent = Mathf.Clamp01(playerHealth.currentHealth / Mathf.Max(1f, playerHealth.maxHealth));
            healthText.text = "HEALTH: " + Mathf.RoundToInt(percent * 100f) + "%";
        }
    }

    // UI
    void CreateUI()
    {
        // Canvas setup
        GameObject canvasGO = new GameObject("GameUI");
        uiCanvas = canvasGO.AddComponent<Canvas>();
        uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Score Text
        scoreText = CreateUIText("ScoreText", new Vector2(0, 1), new Vector2(0, 1), new Vector2(20, -20));
        scoreText.alignment = TextAnchor.UpperLeft;
        scoreText.text = "SCORE: 0";

        // Lives Text
        livesText = CreateUIText("LivesText", new Vector2(1, 1), new Vector2(1, 1), new Vector2(-20, -20));
        livesText.alignment = TextAnchor.UpperRight;
        livesText.text = "LIVES: 3";

        // Health Text
        healthText = CreateUIText("HealthText", new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0, 20));
        healthText.alignment = TextAnchor.LowerCenter;
        healthText.text = "HEALTH: 100%";
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
        text.fontSize = 20;
        text.color = Color.white;


        Outline outline = textGO.AddComponent<Outline>();
        outline.effectColor = new Color(0f, 0.6f, 0f, 0.8f);
        outline.effectDistance = new Vector2(2f, -2f);

        return text;
    }
}
