using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveDataPlayer
{
    public const string FILENAME_SAVEDATA = "/savedata.json";

    public static void SaveGameState()
    {
        string _filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
        PlayerData _playerData = new PlayerData("Player"); //Instance of the file white the data of the player.
        SaveData _saveData = new SaveData(_playerData);
        string _txt = JsonUtility.ToJson(_saveData);
        File.WriteAllText(_filePathSaveData, _txt);
    }
}

[Serializable]
public class SaveData
{
    [SerializeField] PlayerData _playerData;

    public SaveData(PlayerData playerData)
    {
        this._playerData = playerData;
    }
}

[Serializable]
public class PlayerData
{
    [SerializeField] public string _usurname;

    public PlayerData(string usurname)//Set file white the data of the player when the file will be created.
    {
        _usurname = usurname;
    }
}

/* Save :
Username,
Skin,
mail,
diary,
Score mini game,
Relationship monster,
where player is in the story (with last mini game or/and last pnj),
last scene, 
 */