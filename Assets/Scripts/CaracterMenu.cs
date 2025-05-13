using UnityEngine;
using UnityEngine.UI;

public class CaracterMenu : MonoBehaviour
{
    public Dropdown caracterDropdown;

    void Start()
    {
        int savedIndex = PlayerPrefs.GetInt("CaracterIndex", 0);
        caracterDropdown.value = savedIndex;
        CaracterManager.instance.SetCaracter(savedIndex);

        caracterDropdown.onValueChanged.AddListener(delegate {
            CaracterManager.instance.SetCaracter(caracterDropdown.value);
        });
    }
}
