using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ButtonLogger : MonoBehaviour
{
    private Dictionary<string, int> buttonClicks = new Dictionary<string, int>();
    private string filePath;

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "button_usage.txt");
        Debug.Log("Path to log file: " + filePath);
        LoadCountsFromFile();

        DontDestroyOnLoad(gameObject);
    }

    private void LoadCountsFromFile()
    {
        if (!File.Exists(filePath))
            return;

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(':');
            if (parts.Length != 2) continue;

            string key = parts[0].Trim();
            if (int.TryParse(parts[1].Trim(), out int count))
            {
                buttonClicks[key] = count;
            }
        }

        Debug.Log("Loaded button counts from file.");
    }
    public void LogButtonClick(string buttonName)
    {
        if (buttonClicks.ContainsKey(buttonName))
            buttonClicks[buttonName]++;
        else
            buttonClicks[buttonName] = 1;
    }

    void OnApplicationQuit()
    {
        SaveLogToFile();
    }

    private void SaveLogToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var entry in buttonClicks)
            {
                writer.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }

        Debug.Log("Log saved to: " + filePath);
    }
}
