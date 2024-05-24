using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    private const string EXTENSION = ".json";

    public T LoadData<T>(string filePath)
    {
        string path = GetRelativePath(filePath);
        if (File.Exists(path))
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        else
        {
            Debug.Log("NOT EXIST DEFAULT CREATED: " + path);
            return default;
        }
    }

    private string GetRelativePath(string filePath)
    {
        string path = Application.persistentDataPath + "/" + filePath + EXTENSION;
        return path;
    }

    public void SaveData<T>(string filePath, T data)
    {
        string path = GetRelativePath(filePath);
        try
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
    public bool CheckFileExistince(string filePath)
    {
        string path = GetRelativePath(filePath);

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
