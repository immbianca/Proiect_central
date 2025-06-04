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

    private bool isGameSoundPlaying = false;

    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Start()
    {
        if (audioSource != null && background != null)
        {
            audioSource.clip = background;
            audioSource.loop = true;
            audioSource.Play();
        }

    }
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" && !isGameSoundPlaying)
        {
            if (audioSource != null && gameSound != null)
            {
                audioSource.Stop();
                audioSource.clip = gameSound;
                audioSource.Play();
                isGameSoundPlaying = true;
            }
        }
        else if (SceneManager.GetActiveScene().name != "Game" && isGameSoundPlaying)
        {
            if (audioSource != null && background != null)
            {
                audioSource.Stop();
                audioSource.clip = background;
                audioSource.Play();
                isGameSoundPlaying = false;
            }
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
