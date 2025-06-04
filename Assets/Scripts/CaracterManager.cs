using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CaracterManager : MonoBehaviour
{
    public static CaracterManager instance;

    public Sprite[] caracterOptions;
    public AnimatorOverrideController[] caracterAnimators;

    private int selectedCaracterIndex = 0;

    private Image caracterImage;
    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            selectedCaracterIndex = PlayerPrefs.GetInt("CaracterIndex", 0);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedApply());
    }

    IEnumerator DelayedApply()
    {
        yield return null;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            ApplyCaracterTo(playerObj);
        }
    }

    public void SetCaracter(int index)
    {
        if (index < 0 || index >= caracterOptions.Length || index >= caracterAnimators.Length)
            return;

        selectedCaracterIndex = index;
        SaveCaracter();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            ApplyCaracterTo(playerObj);
        }
    }

    public void SaveCaracter()
    {
        PlayerPrefs.SetInt("CaracterIndex", selectedCaracterIndex);
        PlayerPrefs.Save();
    }

    public void ApplyCaracterTo(GameObject playerObj)
    {
        caracterImage = playerObj.GetComponent<Image>();
        animator = playerObj.GetComponent<Animator>();

        ApplyCaracter();
    }

    public void ApplyCaracter()
    {
        Sprite caracterSprite = caracterOptions[selectedCaracterIndex];

        if (caracterImage != null)
            caracterImage.sprite = caracterSprite;

        if (animator != null && caracterAnimators.Length > selectedCaracterIndex)
        {
            animator.runtimeAnimatorController = caracterAnimators[selectedCaracterIndex];
        }
    }
}
