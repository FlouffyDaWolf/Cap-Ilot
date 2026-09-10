using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveAll()
    {
        GameData playerData = new GameData { chrono = testSaveLoad.Instance.chrono };
        SaveSystem.Save(playerData, "player.json");

        Debug.Log("All the files are saved !");
    }
}
