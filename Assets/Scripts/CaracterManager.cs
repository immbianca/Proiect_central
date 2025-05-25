using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaracterManager : MonoBehaviour
{
    public static CaracterManager instance;

    public Sprite[] caracterOptions;
    private int selectedCaracterIndex = 0;

    private Image caracterImage;
    private SpriteRenderer caracterSpriteRenderer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load saved caracter
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
            // Încearcă să obții atât Image cât și SpriteRenderer
            caracterImage = playerObj.GetComponent<Image>();
            caracterSpriteRenderer = playerObj.GetComponent<SpriteRenderer>();

            ApplyCaracter();
        }
    }

    public void SetCaracter(int index)
    {
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
        if (caracterOptions.Length > selectedCaracterIndex)
        {
            if (caracterImage != null)
            {
                caracterImage.sprite = caracterOptions[selectedCaracterIndex];
            }

            if (caracterSpriteRenderer != null)
            {
                caracterSpriteRenderer.sprite = caracterOptions[selectedCaracterIndex];
            }
        }
    }
}
