using UnityEngine;
using UnityEngine.UI;

public class CaracterMenu : MonoBehaviour
{
    public Dropdown caracterDropdown;

    private void Start()
    {
      
        int savedIndex = PlayerPrefs.GetInt("CaracterIndex", 0);

        caracterDropdown.SetValueWithoutNotify(savedIndex);

        if (CaracterManager.instance != null)
        {
            CaracterManager.instance.SetCaracter(savedIndex);
        }
        caracterDropdown.onValueChanged.AddListener(OnCaracterChanged);
    }
    private void OnCaracterChanged(int newIndex)
    {
        if (CaracterManager.instance != null)
        {
            CaracterManager.instance.SetCaracter(newIndex);
        }
    }
}
