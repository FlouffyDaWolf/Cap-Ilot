using System;
using System.IO;
using UnityEngine;

public static class LoadDataPlayer
{
    public static SaveData LoadGameData()
    {
        try
        {
            string filePath = Application.persistentDataPath + SaveDataPlayer.FILENAME_SAVEDATA;
            string fileContent = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(fileContent);
            return saveData;
        }
        catch
        {
            return null;
        }
    }
}
