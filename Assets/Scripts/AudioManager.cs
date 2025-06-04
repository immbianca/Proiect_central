using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source-------")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------Audio Clip--------")]
    public AudioClip background;
    public AudioClip gameSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip enemySound;
    public AudioClip winSound;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        PlayBackground();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game" || scene.name == "Level1")
        {
            PlayGameSound();
        }
        else
        {
            PlayBackground();
        }
    }

    private void PlayBackground()
    {
        if (audioSource != null && background != null)
        {
            audioSource.Stop();
            audioSource.clip = background;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void PlayGameSound()
    {
        if (audioSource != null && gameSound != null)
        {
            audioSource.Stop();
            audioSource.clip = gameSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null && clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
