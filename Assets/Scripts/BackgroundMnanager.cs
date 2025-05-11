
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;

    public Sprite[] backgroundOption;
    public Image backgroundImage;

    private int selectedBackgroundIndex=1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;

            selectedBackgroundIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        backgroundImage=GameObject.FindWithTag("Background").GetComponent<Image>();
        ApplyBackground();
    }

    public void SetBackground(int index)
    {
            selectedBackgroundIndex = index;
            PlayerPrefs.SetInt("BackgroundIndex", selectedBackgroundIndex);
            PlayerPrefs.Save();
    }
    public void ApplyBackground()
    {
        if (backgroundImage != null && backgroundOption.Length>selectedBackgroundIndex)
        {
            backgroundImage.sprite = backgroundOption[selectedBackgroundIndex];
        }
    }
}
