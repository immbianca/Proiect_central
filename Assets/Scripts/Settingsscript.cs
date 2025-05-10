using System.Collections;
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

    private void Start()
    {
        AllRezolutions = Screen.resolutions;

        List<string> resolutiomStringList = new List<string>();
        string newRes;

        int currentResolutionIndex = 0;
        for (int i = 0; i < AllRezolutions.Length; i++)
        {
            Resolution resolution = AllRezolutions[i];
            newRes = resolution.width.ToString() + " x " + resolution.height.ToString();
            if (!resolutiomStringList.Contains(newRes))
            {
                resolutiomStringList.Add(newRes);
                SelectedResolutionList.Add(resolution);

              
                if (resolution.width == Screen.width && resolution.height == Screen.height)
                {
                    currentResolutionIndex = SelectedResolutionList.Count - 1;
                }
            }
        }

        Rezolutiedrop.AddOptions(resolutiomStringList);

        Rezolutiedrop.value = currentResolutionIndex;
        SelectedRezolution = currentResolutionIndex;

        isFullscreen = Screen.fullScreen;
        Fullscreen.isOn = isFullscreen;
    }

    public void SetResolution()
    {
        SelectedRezolution = Rezolutiedrop.value;
        Screen.SetResolution(SelectedResolutionList[SelectedRezolution].width, SelectedResolutionList[SelectedRezolution].height, isFullscreen);
    }

    public void SetFullscreen()
    {
        isFullscreen = Fullscreen.isOn;
        Screen.SetResolution(SelectedResolutionList[SelectedRezolution].width, SelectedResolutionList[SelectedRezolution].height, isFullscreen);
    }

    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
