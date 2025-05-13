using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaracterManager : MonoBehaviour
{
    public static CaracterManager instance;

    public Sprite[] caracterOptions;
    private int selectedCaracterIndex = 0;
    private Image caracterImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (caracterImage != null && caracterOptions.Length > selectedCaracterIndex)
        {
            caracterImage.sprite = caracterOptions[selectedCaracterIndex];
        }
    }
}
