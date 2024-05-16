using System;
using System.IO;
using Newtonsoft.Json;

public static class JsonSaver
{
    public const string SYMBOL_DATA_PATH = "Assets/Resources/slotSymbolData.json";
    public const string CREATED_RESULTS_FILE_PATH = "Assets/Resources/createdResults.json";
    public const string ALL_SPIN_RESULTS_FILE_PATH = "Assets/Resources/allSpinResults.json";
    public const string CURRENT_SPIN_FILE_PATH = "Assets/Resources/currentSpin.json";
    public static void SaveData<T>(T data, string filePath)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }

    public static T LoadData<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        else
        {
            return default;
        }
    }
}
