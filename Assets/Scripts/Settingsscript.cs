using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settingsscript : MonoBehaviour
{
    public Dropdown Rezolutiedrop;
    public Toggle Fullscreen;

    Resolution[] AllRezolutions;
    bool isFullscreen;
    int SelectedRezolution;
    List<Resolution> SelectedResolutionList = new List<Resolution>();

    void Start()
    {
        AllRezolutions = Screen.resolutions;
        List<string> resolutionStringList = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < AllRezolutions.Length; i++)
        {
            Resolution res = AllRezolutions[i];
            string resString = res.width + " x " + res.height;
            if (!resolutionStringList.Contains(resString))
            {
                resolutionStringList.Add(resString);
                SelectedResolutionList.Add(res);

                if (res.width == Screen.width && res.height == Screen.height)
                    currentResolutionIndex = SelectedResolutionList.Count - 1;
            }
        }

        Rezolutiedrop.AddOptions(resolutionStringList);

        if (PlayerPrefs.HasKey("resolutionIndex"))
            currentResolutionIndex = PlayerPrefs.GetInt("resolutionIndex");
        if (PlayerPrefs.HasKey("isFullscreen"))
            isFullscreen = PlayerPrefs.GetInt("isFullscreen") == 1;
        else
            isFullscreen = Screen.fullScreen;

        Rezolutiedrop.value = currentResolutionIndex;
        SelectedRezolution = currentResolutionIndex;
        Fullscreen.isOn = isFullscreen;

        Screen.SetResolution(SelectedResolutionList[currentResolutionIndex].width, SelectedResolutionList[currentResolutionIndex].height, isFullscreen);
    }

    public void SetResolution()
    {
        SelectedRezolution = Rezolutiedrop.value;
        ApplySettings();
        PlayerPrefs.SetInt("resolutionIndex", SelectedRezolution);
        PlayerPrefs.Save();
    }

    public void SetFullscreen()
    {
        isFullscreen = Fullscreen.isOn;
        ApplySettings();
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    void ApplySettings()
    {
        Screen.SetResolution(SelectedResolutionList[SelectedRezolution].width, SelectedResolutionList[SelectedRezolution].height, isFullscreen);
    }

    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

