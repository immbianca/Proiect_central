using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;  

    public void ValidateName()
    {
        string name = inputField.text;
        if (string.IsNullOrEmpty(name) || name.Length < 4)
        {
            resultText.text = "Numele trebuie să aibă minim 4 caractere.";
            resultText.color = Color.red;
            return;
        }
        else
        {
            resultText.text = "Numele este valid.";
            resultText.color = Color.green;
            PlayerPrefs.SetString("Nume", name);
            return;
        }
    }

    public void ValidateEmail()
    {
        string email = inputField.text;
        if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains("."))
        {
            resultText.text = "Email invalid.";
            resultText.color = Color.red;
            return;
        }
        else
        {
            resultText.text = "Email valid.";
            resultText.color = Color.green;
            PlayerPrefs.SetString("Email", email);
            return;
        }
    }

    public void ValidatePassword()
    {
        string password = inputField.text;
        if (!validare(password))
        {
            resultText.text = "Parola trebuie să conțină cel puțin 5 caractere, o majusculă, o minusculă și o cifră.";
            resultText.color = Color.red;
            return;
        }
        else
        {
            resultText.text = "Parola validă.";
            resultText.color = Color.green;
            PlayerPrefs.SetString("Parola", password);
            return;
        }
    }

    bool validare(string parola)
    {
        if(string.IsNullOrEmpty(parola)|| parola.Length<5) return false;

        bool areMajuscule = false;
        bool areMinuscule = false;
        bool areCifre = false;

        foreach(char c in parola)
        {
            if (char.IsUpper(c)) areMajuscule = true;
            else if (char.IsLower(c)) areMinuscule = true;
            else if (char.IsDigit(c)) areCifre = true;
        }

        return areMajuscule && areMinuscule && areCifre;
    }

    public void Salvare()
    {
        PlayerPrefs.Save();
        CaracterManager.instance.SaveCaracter();
        resultText.color = Color.green;
        resultText.text = "Datele au fost salvate.";

    }

    public void Meniu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Joc() {
        SceneManager.LoadScene("Game");
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Nume"))
        {
            InputField input1 = GameObject.Find("NumeField").GetComponent<InputField>();
            input1.text = PlayerPrefs.GetString("Nume");
        }
        if (PlayerPrefs.HasKey("Email"))
        {
            InputField input2 = GameObject.Find("EmailField").GetComponent<InputField>();
            input2.text = PlayerPrefs.GetString("Email");
        }
        if (PlayerPrefs.HasKey("Parola"))
        {
            InputField input3 = GameObject.Find("ParolaField").GetComponent<InputField>();
            input3.text = PlayerPrefs.GetString("Parola");
        }
    }
}
