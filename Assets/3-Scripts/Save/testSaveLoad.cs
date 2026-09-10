using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public float chrono;
}

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

    public void LoadGame()
    {
        SaveData saveData = LoadDataPlayer.LoadGameData();
        if (saveData != null)
        {
            chrono = saveData._playerData._chrono;
            Debug.Log("Loaded Game !");
        }
        else
        {
            Debug.LogWarning("No save found.");
        }
    }
}