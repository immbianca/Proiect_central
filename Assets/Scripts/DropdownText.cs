using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropdownText : MonoBehaviour
{
    public Dropdown textDropdown;
    void Start()
    {
        int savedIndex = PlayerPrefs.GetInt("SelectedFontIndex", 0);
        textDropdown.value = savedIndex;
        textDropdown.onValueChanged.AddListener(delegate
        {
            TextManager.instance.SetFont(textDropdown.value);
        });
    }
        
 }
