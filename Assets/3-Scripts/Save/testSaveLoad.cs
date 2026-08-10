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

    void Start()
    {
        chrono = 0;
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
