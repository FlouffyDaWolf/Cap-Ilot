using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PNJData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public List<Conversation> Conversations { get; private set; }
    public int CurrentConversationIndex { get; set; }
}
