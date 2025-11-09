using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button startButton;
    public Button settingsButton;
    public Button exitButton;

    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Button backButton;

    void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Button Functions
        if (startButton != null) startButton.onClick.AddListener(StartGame);
        if (settingsButton != null) settingsButton.onClick.AddListener(OpenSettings);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);
        if (backButton != null) backButton.onClick.AddListener(CloseSettings);

        // Slider setup
        if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        if (sensitivitySlider != null) sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);

        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("Volume", v));
        if (sensitivitySlider != null) sensitivitySlider.onValueChanged.AddListener((s) => PlayerPrefs.SetFloat("Sensitivity", s));
    }

    public void StartGame()
    {
        // Load Gameplay
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
