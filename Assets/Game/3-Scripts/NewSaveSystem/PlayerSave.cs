using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct MiniGameSave
{
    public float Chrono;
    public bool Unlocked;
}

public struct PlayerInfo
{
    public string PlayerName;
    public DateTime BirthDate;
    public string NurseEmail;
}

[Serializable]
public class SerializableTypePair<TKey, TValue>
{
    public TKey Key;
    public TValue Value;
}

[Serializable]
public class PlayerSave
{
    [field: SerializeField] public PlayerInfo PlayerInfo { get; set; }
    [field: SerializeField] public List<SerializableTypePair<string, MiniGameSave>> MiniGameSaves { get; private set; } = new();
    [field: SerializeField] public List<Page> Pages { get; private set; } = new();
}

// Must implement :
// - skin
// - house repair progression
// - player interactions
// - maybe last scene where player was ?