using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UsageLogger : MonoBehaviour
{
    private Dictionary<string, int> buttonUsage = new Dictionary<string, int>();
    private string filePath;

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "button_usage.txt");

        // Încarcă valorile existente dacă fișierul există deja
        if (File.Exists(filePath))
        {
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    int value;
                    if (int.TryParse(parts[1], out value))
                        buttonUsage[key] = value;
                }
            }
        }
    }

    public void LogFromButton(string buttonID)
    {
        if (!buttonUsage.ContainsKey(buttonID))
            buttonUsage[buttonID] = 0;

        buttonUsage[buttonID]++;

        SaveToFile();
    }

    private void SaveToFile()
    {
        List<string> lines = new List<string>();
        foreach (var entry in buttonUsage)
        {
            lines.Add($"{entry.Key}: {entry.Value}");
        }

        File.WriteAllLines(filePath, lines.ToArray());
        File.WriteAllText(filePath, "TEST SCRIERE");

    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application is quitting. Saving usage data...");
        Debug.Log("Usage data saved to: " + filePath);
    }
}
