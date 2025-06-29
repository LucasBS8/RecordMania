using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip coinSound;

    [Header("Settings")]
    public float masterVolume = 1f;
    public bool isMuted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void PlaySFX(AudioClip clip) { /* Implementar */ }
    //public void SetVolume(float volume) { /* Implementar */ }
    public void ToggleMute() { /* Implementar */ }

    public void SetVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;
        if (sfxSource != null)
            sfxSource.volume = value;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }

        if (musicSource.isPlaying && musicSource == backgroundMusic)
        {
            return;
        }
    }
}