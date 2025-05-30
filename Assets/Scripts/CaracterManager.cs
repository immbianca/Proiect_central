using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaracterManager : MonoBehaviour
{
    public static CaracterManager instance;

    public Sprite[] caracterOptions;
    public AnimatorOverrideController[] caracterAnimators;

    private int selectedCaracterIndex = 0;

    private Image caracterImage;
    private SpriteRenderer caracterSpriteRenderer;
    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            selectedCaracterIndex = PlayerPrefs.GetInt("CaracterIndex", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            caracterImage = playerObj.GetComponent<Image>();
            caracterSpriteRenderer = playerObj.GetComponent<SpriteRenderer>();
            animator = playerObj.GetComponent<Animator>();

            ApplyCaracter();
        }
    }

    public void SetCaracter(int index)
    {
        if (index < 0 || index >= caracterOptions.Length || index >= caracterAnimators.Length)
            return;
        selectedCaracterIndex = index;
        ApplyCaracter();
    }

    public void SaveCaracter()
    {
        PlayerPrefs.SetInt("CaracterIndex", selectedCaracterIndex);
        PlayerPrefs.Save();
    }

    public void ApplyCaracter()
    {
        Sprite caracterSprite = caracterOptions[selectedCaracterIndex];

        if (caracterImage != null)
            caracterImage.sprite = caracterSprite;

        if (caracterSpriteRenderer != null)
            caracterSpriteRenderer.sprite = caracterSprite;

        if (animator != null && caracterAnimators.Length > selectedCaracterIndex)
        {
            animator.runtimeAnimatorController = caracterAnimators[selectedCaracterIndex];
        }
    }
}
