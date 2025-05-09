using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
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
}
