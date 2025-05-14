using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;

    public Sprite[] backgroundOptions;        
    private int selectedBackgroundIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
            SceneManager.sceneLoaded += OnSceneLoaded;

            selectedBackgroundIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyBackground(); 
    }

    public void SetBackground(int index)
    {
        selectedBackgroundIndex = index;
        ApplyBackground(); 
    }

    public void SaveBackground()
    {
        PlayerPrefs.SetInt("BackgroundIndex", selectedBackgroundIndex);
        PlayerPrefs.Save();
    }

    public void ApplyBackground()
    {
        GameObject bgObject = GameObject.FindWithTag("Background");
        if (bgObject != null)
        {
            Image backgroundImage = bgObject.GetComponent<Image>();
            if (backgroundImage != null && backgroundOptions.Length > selectedBackgroundIndex)
            {
                backgroundImage.sprite = backgroundOptions[selectedBackgroundIndex];
            }
        }
    }
}
