using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    private static bool initialized = false;
    public void playButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void settingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void characterCreate()
    {
        SceneManager.LoadScene("CaracterCreator");
    }
    public void quitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    void Awake()
    {
        if (!initialized)
        {
            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            initialized = true;
        } 
    }

}
