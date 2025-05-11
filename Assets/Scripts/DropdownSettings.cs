using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSettings : MonoBehaviour
{
    public Dropdown background;

    public void Start()
    {
        int savedIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        background.value = savedIndex;

        background.onValueChanged.AddListener(delegate
        {
            BackgroundManager.instance.SetBackground(background.value);
        });
    }

}
