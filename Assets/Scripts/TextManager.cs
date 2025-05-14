using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    public Font[] textOptions;
    private int selectedTextIndex = 0;

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
        ApplyFont();
    }

    public void SetFont(int index)
    {
        selectedTextIndex = index;
        ApplyFont(); 
    }

    public void SaveFont()
    {
        PlayerPrefs.SetInt("SelectedFontIndex", selectedTextIndex);
        PlayerPrefs.Save();
    }

    public void ApplyFont()
    {
        Text[] allTexts = Resources.FindObjectsOfTypeAll<Text>();
        foreach (Text text in allTexts)
        {
            if (text != null && textOptions.Length > selectedTextIndex)
            {
                text.font = textOptions[selectedTextIndex];
            }
        }
    }
}
