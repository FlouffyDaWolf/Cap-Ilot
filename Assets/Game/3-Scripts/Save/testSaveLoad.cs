using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSaveLoad : MonoBehaviour
{
    public float chrono;
    public static testSaveLoad Instance;

    private void Awake()
    {
        if (Instance == null )
        {
            Instance = this;
        }
    }

    private void Start()
    {
        chrono = 0;

        SaveData savedata = LoadDataPlayer.LoadGameData();
        if (savedata != null)
        {
            chrono = savedata._playerData._chrono;
        }
    }

    void Update()
    {
        chrono += Time.deltaTime;
        Debug.Log(chrono);
    }

    private void OnApplicationPause( bool pauseStatue )
    {
        if (pauseStatue)
            SaveDataPlayer.SaveGameState();
    }

    private void OnApplicationQuit()
    {
        SaveDataPlayer.SaveGameState();
    }
}
