using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void Save<T>(T data, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"Data save in : {filePath}");
    }

    public static T Load<T>(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"File not found : {filePath}");
            return default;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading {fileName} : {e.Message}");
            return default;
        }
    }
}
