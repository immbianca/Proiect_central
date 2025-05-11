using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    public Font[] textOptions;
    public Text testmodify;

    public int selectedTextIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            selectedTextIndex = PlayerPrefs.GetInt("SelectedFontIndex", 0);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyFont();
    }
    public void SetFont(int index)
    {
        selectedTextIndex = index;
        PlayerPrefs.SetInt("SelectedFontIndex", selectedTextIndex);
        PlayerPrefs.Save();
        ApplyFont();
    }

    public void ApplyFont()
    {
        Text[] alLTexts = FindObjectsOfType<Text>(true);
        foreach (Text text in alLTexts)
        {
            if (text != null && textOptions.Length > selectedTextIndex)
            {
                text.font = textOptions[selectedTextIndex];
            }
        }
    }
}
