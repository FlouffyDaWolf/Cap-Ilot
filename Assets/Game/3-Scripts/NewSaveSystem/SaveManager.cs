using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    static readonly string _fileLocation = Application.persistentDataPath + "/savedata.json";

    public static PlayerSave PlayerSave { get; private set; } = new();

    public static void InitPlayerInfo(PlayerInfo playerInfo)
    {
        PlayerSave.PlayerInfo = playerInfo;
    }

    public static bool LoadGameData()
    {
        try
        {
            string fileContent = File.ReadAllText(_fileLocation);
            PlayerSave = JsonUtility.FromJson<PlayerSave>(fileContent);
            return true;
        }
        catch { return false; }
    }

    public static void SaveGameState() => File.WriteAllText(_fileLocation, JsonUtility.ToJson(PlayerSave));
}