using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSaveLoad : MonoBehaviour
{
    public float chrono;
    public static testSaveLoad Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        chrono = 0;
    }

    void Update()
    {
        chrono += Time.deltaTime;
        Debug.Log(chrono);
    }

    public void SaveGame()
    {
        SaveDataPlayer.SaveGameState();
        Debug.Log("Partie sauvegardée !");
    }

    public void LoadGame()
    {
        SaveData saveData = LoadDataPlayer.LoadGameData();
        if (saveData != null)
        {
            chrono = saveData._playerData._chrono;
            Debug.Log("Partie chargée !");
        }
        else
        {
            Debug.LogWarning("Aucune sauvegarde trouvée.");
        }
    }
}