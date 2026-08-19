//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using UnityEngine;

//public static class buttonSave
//{
//    public const string FILENAME_SAVEDATA = "/savedata.json";

//    public static void SaveGameState()
//    {
//        string _filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
//        PlayerData _playerData = new PlayerData(testSaveLoad.Instance);
//        SaveData _saveData = new SaveData(_playerData);
//        string _txt = JsonUtility.ToJson(_saveData);
//        File.WriteAllText(_filePathSaveData, _txt);
//    }
//}

///* Save :
//Username,
//Skin,
//mail,
//diary,
//Score mini game,
//Relationship monster,
//where player is in the story (with last mini game or/and last pnj),
//last scene, 
// */