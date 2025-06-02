using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameScript : MonoBehaviour
{
    [SerializeField] private GameObject menu;

        private void Start()
    {
        Time.timeScale = 1f;
    }
    public void Pause()

    {
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
      SceneManager.LoadScene("MainMenu");
    }
}
