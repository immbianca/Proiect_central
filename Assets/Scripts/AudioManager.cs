using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source-------")]
    [SerializeField] AudioSource audioSource;

    [Header("------Audio Clip--------")]
    public AudioClip background;

    private static AudioManager instance;
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
        if (SceneManager.GetActiveScene().name == "Game")
        {
          Destroy(gameObject);
          return;
        }
    }

}
