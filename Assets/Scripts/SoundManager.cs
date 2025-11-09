using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource humSource;

    [Header("Audio Clips")]
    public AudioClip bgMusic;
    public AudioClip blasterClip;
    public AudioClip explosionClip;

    [Header("UI Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Load saved volumes
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Apply values
        if (musicSource != null)
        {
            musicSource.clip = bgMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        if (humSource != null)
        {
            humSource.loop = true;
            humSource.spatialBlend = 1f; // make it 3D
            humSource.dopplerLevel = 2f;
            humSource.Play();
        }
    }

    void Update()
    {
        // Adjust hum volume based on distance from player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && humSource != null)
        {
            float distance = Vector3.Distance(player.transform.position, humSource.transform.position);
            humSource.volume = Mathf.Clamp(1 / distance, 0.1f, 1f);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void PlayBlaster()
    {
        if (sfxSource != null && blasterClip != null)
        {
            sfxSource.PlayOneShot(blasterClip, sfxVolume);
        }
    }

    public void PlayExplosion()
    {
        if (sfxSource != null && explosionClip != null)
        {
            sfxSource.PlayOneShot(explosionClip, sfxVolume);
        }
    }
}
